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

            var connectionString = configuration.GetConnectionString("WorldIndexes"); 

            services.AddDbContext<WorldDataContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                options.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
            });

            services.AddScoped<DbContext, WorldDataContext>();

            services.AddUnitOfWork();
            services.AddUnitOfWork<WorldDataContext>();

            return services;
        }
    }
}