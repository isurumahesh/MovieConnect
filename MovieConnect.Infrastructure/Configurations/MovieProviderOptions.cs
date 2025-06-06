namespace MovieConnect.Infrastructure.Configurations
{
    public class MovieProviderOptions
    {
        public List<MovieDetailProvider> MovieDetailProviders { get; set; }
        public List<MovieVideoProvider> MovieVideoProviders { get; set; }
    }
}