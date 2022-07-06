using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;

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
                .AddPolicyHandler(GetDefaultRetryPolicy(maxRetryAttempts))
                .AddPolicyHandler(GetDefaultCircuitBreakerPolicy(maxConsecutiveFailures, maxCircuitBreakerWaitingTime));

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

        private static IAsyncPolicy<HttpResponseMessage> GetDefaultRetryPolicy(int maxRequestAttempt)
            => HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(maxRequestAttempt, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        private static IAsyncPolicy<HttpResponseMessage> GetDefaultCircuitBreakerPolicy(int maxConsecutiveFailures, int maxCircuitBreakerWaitingTime)
            => HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: maxConsecutiveFailures,
                    durationOfBreak: TimeSpan.FromSeconds(maxCircuitBreakerWaitingTime),
                    onBreak: (delegateResult, timeSpan, context) => { },
                    onReset: (context) => { });
    }
}