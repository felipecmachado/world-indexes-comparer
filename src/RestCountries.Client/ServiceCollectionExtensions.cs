using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using WorldIndexesComparer.Common.Http.Policies;

namespace RestCountries.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestCountriesClient(this IServiceCollection services,
                                                          string baseUri,
                                                          int maxRetryAttempts,
                                                          int maxConsecutiveFailures,
                                                          int maxCircuitBreakerWaitingTime,
                                                          TimeSpan timeout = default)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddHttpClient(nameof(IRestCountriesClient), configureClient: httpClient => ConfigureClient(httpClient, baseUri, timeout))
                .AddPolicyHandler(PollyPolicies.GetDefaultRetryPolicy(maxRetryAttempts))
                .AddPolicyHandler(PollyPolicies.GetDefaultCircuitBreakerPolicy(maxConsecutiveFailures, maxCircuitBreakerWaitingTime));

            services.AddScoped<IRestCountriesClient, RestCountriesClient>();

            return services;
        }

        private static void ConfigureClient(HttpClient httpClient, string baseUri, TimeSpan? timeout = null)
        {
            httpClient.BaseAddress = new Uri($"{baseUri}/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (timeout.HasValue)
            {
                httpClient.Timeout = timeout.Value;
            }
        }
    }
}