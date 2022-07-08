using Covid19.Client;
using RestCountries.Client;

namespace WorldIndexesComparer.BackgroundServices.Configurations
{
    public static class ClientsConfiguration
    {
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