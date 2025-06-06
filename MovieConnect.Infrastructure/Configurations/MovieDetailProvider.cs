namespace MovieConnect.Infrastructure.Configurations
{
    public class MovieDetailProvider
    {
        public string Name { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string ApiUrl { get; set; } = null!;
        public bool IsActive { get; set; }
        public HttpClientPoliciesOptions HttpClientPolicies { get; set; } = null!;
    }
}