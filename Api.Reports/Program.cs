namespace Api.Reports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.MapOpenApi();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "CQRS API v1");
                options.RoutePrefix = "swagger";
            });

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