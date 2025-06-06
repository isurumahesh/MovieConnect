using MovieConnect.Application.Interfaces;
using MovieConnect.Core.Interfaces;

namespace MovieConnect.Application.Services
{
    public class MovieDetailServiceSelector : IMovieDetailServiceSelector
    {
        private readonly Dictionary<string, IMovieDetailService> services;
        private readonly string selectedProvider;

        public MovieDetailServiceSelector(IEnumerable<IMovieDetailService> services, IMovieProviderConfiguration providerConfiguration)
        {
            this.services = services.ToDictionary(s => s.ProviderName, StringComparer.OrdinalIgnoreCase);
            this.selectedProvider = providerConfiguration.GetActiveMovieDetailProvider();
        }

        public IMovieDetailService GetService()
        {
            if (string.IsNullOrEmpty(selectedProvider))
            {
                throw new ArgumentException($"Movie details provider is not defined");
            }

            if (services.TryGetValue(selectedProvider, out var service))
                return service;

            throw new ArgumentException($"Service '{selectedProvider}' not found.");
        }
    }
}