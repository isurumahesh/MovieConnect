namespace MovieConnect.Infrastructure.Configurations
{
    public class HttpClientPoliciesOptions
    {
        public int RetryCount { get; set; }
        public int RetryDelaySeconds { get; set; }
        public int CircuitBreakerFailureThreshold { get; set; }
        public int CircuitBreakerDurationSeconds { get; set; }
    }
}