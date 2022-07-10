using Covid19.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestCountries.Client;
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

        public static IServiceCollection AddCQRSConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);

            return services;
        }

        public static IServiceCollection AddClientsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddCovid19Client(
                baseUri: configuration.GetValue<string>("Clients:Covid19:BaseUri"),
                maxRetryAttempts: configuration.GetValue<int>("Clients:Covid19:MaxRetryAttempts"),
                maxConsecutiveFailures: configuration.GetValue<int>("Clients:Covid19:MaxConsecutiveFailures"),
                maxCircuitBreakerWaitingTime: configuration.GetValue<int>("Clients:Covid19:MaxCircuitBreakerWaitingTime"),
                timeout: configuration.GetValue<TimeSpan>("Clients:Covid19:Timeout"));

            services.AddRestCountriesClient(
                baseUri: configuration.GetValue<string>("Clients:RestCountries:BaseUri"),
                maxRetryAttempts: configuration.GetValue<int>("Clients:RestCountries:MaxRetryAttempts"),
                maxConsecutiveFailures: configuration.GetValue<int>("Clients:RestCountries:MaxConsecutiveFailures"),
                maxCircuitBreakerWaitingTime: configuration.GetValue<int>("Clients:RestCountries:MaxCircuitBreakerWaitingTime"),
                timeout: configuration.GetValue<TimeSpan>("Clients:RestCountries:Timeout"));

            return services;
        }
    }
}