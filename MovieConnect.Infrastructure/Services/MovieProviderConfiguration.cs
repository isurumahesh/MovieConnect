using Microsoft.Extensions.Options;
using MovieConnect.Application.Interfaces;
using MovieConnect.Infrastructure.Configurations;

namespace MovieConnect.Infrastructure.Services
{
    public class MovieProviderConfiguration : IMovieProviderConfiguration
    {
        private readonly MovieProviderOptions options;

        public MovieProviderConfiguration(IOptions<MovieProviderOptions> options)
        {
            this.options = options.Value;
        }

        public string GetActiveMovieDetailProvider()
        {
            return options.MovieDetailProviders.First(a => a.IsActive).Name;
        }

        public string GetActiveMovieVideoProvider()
        {
            return options.MovieVideoProviders.First(a => a.IsActive).Name;
        }
    }
}