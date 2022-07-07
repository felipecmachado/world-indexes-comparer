using Microsoft.EntityFrameworkCore;

namespace WorldIndexesComparer.Infrastructure.Data.Contexts
{
    public class WorldDataContext : DbContext
    {
        public WorldDataContext(DbContextOptions<WorldDataContext> options)
           : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(WorldDataContext).Assembly);
        }
    }
}
