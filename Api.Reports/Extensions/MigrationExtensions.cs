using Microsoft.EntityFrameworkCore;
using reports.infrastructure.Data.Context;
using reports.infrastructure.Data.Seed;

namespace Api.Reports.Extensions
{
    public static class MigrationExtensions
    {
        public static async Task<WebApplication >ApplyMigrations(this WebApplication app)
        {
            if (app.Environment.IsProduction())
                return app;

            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<PostgresContext>();

            Console.WriteLine("Applying migrations...");
            context.Database.Migrate();

            Console.WriteLine("Seeding database...");
            await DbSeeder.SeedAsync(context);

            Console.WriteLine("Database ready.");

            return app;
        }
    }
}
