using Polly;
using Polly.Extensions.Http;

namespace MovieConnect.Infrastructure.Configurations
{
    public class HttpClientPolicyBuilder
    {
        public static IAsyncPolicy<HttpResponseMessage> GetPolicy(HttpClientPoliciesOptions? policies)
        {
            if (policies == null)
                return Policy.NoOpAsync<HttpResponseMessage>();

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(policies.RetryCount, _ => TimeSpan.FromSeconds(policies.RetryDelaySeconds));

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(policies.CircuitBreakerFailureThreshold, TimeSpan.FromSeconds(policies.CircuitBreakerDurationSeconds));

            return Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
        }
    }
}