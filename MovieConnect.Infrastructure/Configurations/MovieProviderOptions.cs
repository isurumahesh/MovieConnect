namespace MovieConnect.Infrastructure.Configurations
{
    public class MovieProviderOptions
    {
        public List<MovieDetailProvider> MovieDetailProviders { get; set; }=new List<MovieDetailProvider>();
        public List<MovieVideoProvider> MovieVideoProviders { get; set; } = new List<MovieVideoProvider>();
    }
}