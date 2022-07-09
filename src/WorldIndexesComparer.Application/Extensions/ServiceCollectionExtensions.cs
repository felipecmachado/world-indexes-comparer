using Microsoft.Extensions.DependencyInjection;
using WorldIndexesComparer.Application.Services;
using WorldIndexesComparer.Application.Services.Interfaces;

namespace WorldIndexesComparer.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<ICountriesProcessingAppService, CountriesProcessingAppService>();
            services.AddTransient<ICoronavirusDataProcessingAppService, CoronavirusDataProcessingAppService>();

            return services;
        }
    }
}