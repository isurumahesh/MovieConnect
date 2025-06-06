namespace MovieConnect.Infrastructure.Configurations
{
    public class MovieDetailProvider
    {
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
        public bool IsActive { get; set; }
        public HttpClientPoliciesOptions HttpClientPolicies { get; set; }
    }
}