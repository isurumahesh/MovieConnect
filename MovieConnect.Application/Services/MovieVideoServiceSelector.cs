using MovieConnect.Application.Interfaces;
using MovieConnect.Core.Interfaces;

namespace MovieConnect.Application.Services
{
    public class MovieVideoServiceSelector : IMovieVideoServiceSelector
    {
        private readonly Dictionary<string, IMovieVideoService> services;
        private readonly string selectedProvider;

        public MovieVideoServiceSelector(IEnumerable<IMovieVideoService> services, IMovieProviderConfiguration providerConfiguration)
        {
            this.services = services.ToDictionary(s => s.ProviderName, StringComparer.OrdinalIgnoreCase);
            this.selectedProvider = providerConfiguration.GetActiveMovieVideoProvider();
        }

        public IMovieVideoService GetService()
        {
            if (string.IsNullOrEmpty(selectedProvider))
            {
                throw new ArgumentException($"Movie video provider is not defined");
            }

            if (services.TryGetValue(selectedProvider, out var service))
                return service;

            throw new ArgumentException($"Service '{selectedProvider}' not found.");
        }
    }
}