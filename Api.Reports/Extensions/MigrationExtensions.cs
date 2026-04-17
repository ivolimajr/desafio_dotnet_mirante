using Microsoft.EntityFrameworkCore;
using reports.infrastructure.Data.Context;

namespace Api.Reports.Extensions
{
    public static class MigrationExtensions
    {
        public static WebApplication ApplyMigrations(this WebApplication app)
        {
            if (app.Environment.IsProduction())
                return app;

            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<PostgresContext>();

            context.Database.Migrate();

            return app;
        }
    }
}
