using Microsoft.EntityFrameworkCore;

namespace reports.infrastructure.Data.Context
{
    public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("mirante-reports");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresContext).Assembly);
        }
    }
}
