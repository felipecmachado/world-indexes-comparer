using Microsoft.Extensions.DependencyInjection;
using WorldIndexesComparer.Application.Countries.Services;
using WorldIndexesComparer.Application.Countries.Services.Interfaces;

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

            return services;
        }
    }
}