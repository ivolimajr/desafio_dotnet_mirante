using Api.Reports.Extensions;
using Microsoft.EntityFrameworkCore;
using reports.application.Services;
using reports.domain.Interfaces;
using reports.infrastructure.Data.Context;
using reports.infrastructure.Repositories;

namespace Api.Reports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<PostgresContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "mirante-reports")
            ));

            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddTransient<ITaskService, TaskService>();

            var app = builder.Build();

            app.MapOpenApi();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "CQRS API v1");
                options.RoutePrefix = "swagger";
            });


            Console.WriteLine("Api Running...." + app.Environment.EnvironmentName);

            if (app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.ApplyMigrations().GetAwaiter().GetResult();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", () => Results.Redirect("/swagger"));

            app.Run();
        }
    }
}