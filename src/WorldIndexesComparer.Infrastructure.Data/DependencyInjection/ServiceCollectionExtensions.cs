using EntityFrameworkCore.UnitOfWork.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldIndexesComparer.Infrastructure.Data.Contexts;

namespace WorldIndexesComparer.Infrastructure.Data.Modules
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var connectionString = configuration.GetConnectionString("WorldData"); 

            services.AddDbContext<WorldDataContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                options.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
            });

            services.AddScoped<DbContext, WorldDataContext>();

            services.AddUnitOfWork();
            services.AddUnitOfWork<WorldDataContext>();

            services.EnsureDatabaseIsCreated();

            return services;
        }

        private static void EnsureDatabaseIsCreated(this IServiceCollection services)
        {
            var serviceScopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

            using var serviceScope = serviceScopeFactory.CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetService<WorldDataContext>();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}