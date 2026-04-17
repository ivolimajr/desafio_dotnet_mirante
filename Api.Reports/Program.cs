using Microsoft.EntityFrameworkCore;
using reports.infrastructure.Data.Context;

namespace Api.Reports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();


            //Debug TODO:Remover após testes
            Console.WriteLine("Connection:" + builder.Configuration.GetConnectionString("DefaultConnection"));


            builder.Services.AddDbContext<PostgresContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "mirante-reports")
            ));


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

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", () => Results.Redirect("/swagger"));

            app.Run();
        }
    }
}