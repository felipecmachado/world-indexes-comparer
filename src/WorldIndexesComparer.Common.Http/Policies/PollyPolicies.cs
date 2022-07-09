using Polly;
using Polly.Extensions.Http;

namespace WorldIndexesComparer.Common.Http.Policies
{
    public static class PollyPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetDefaultRetryPolicy(int maxRequestAttempt)
            => HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(maxRequestAttempt, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        public static IAsyncPolicy<HttpResponseMessage> GetDefaultCircuitBreakerPolicy(int maxConsecutiveFailures, int maxCircuitBreakerWaitingTime)
            => HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: maxConsecutiveFailures,
                    durationOfBreak: TimeSpan.FromSeconds(maxCircuitBreakerWaitingTime),
                    onBreak: (delegateResult, timeSpan, context) => { },
                    onReset: (context) => { });
    }
}
