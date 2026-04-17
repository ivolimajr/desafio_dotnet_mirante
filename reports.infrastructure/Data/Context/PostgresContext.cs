using Microsoft.EntityFrameworkCore;
using reports.domain.Entities;

namespace reports.infrastructure.Data.Context
{
    public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("mirante-reports");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresContext).Assembly);
        }
    }
}
