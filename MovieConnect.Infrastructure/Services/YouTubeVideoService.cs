using AutoMapper;
using Microsoft.Extensions.Options;
using MovieConnect.Core.Interfaces;
using MovieConnect.Core.Models;
using MovieConnect.Infrastructure.ApiResponse;
using MovieConnect.Infrastructure.Configurations;
using System.Net.Http.Json;

namespace MovieConnect.Infrastructure.Services
{
    public class YouTubeVideoService(HttpClient httpClient, IMapper mapper, IOptions<MovieProviderOptions> options) : IMovieVideoService
    {
        public string ProviderName => "youtube";

        public async Task<List<MovieVideo>> GetMovieVideosAsync(string movieName)
        {
            if (string.IsNullOrWhiteSpace(movieName))
                throw new ArgumentException("Movie name must be provided.", nameof(movieName));

            var provider = options.Value.MovieVideoProviders.FirstOrDefault(a => a.Name == ProviderName);

            if (provider is null || string.IsNullOrEmpty(provider.ApiKey))
            {
                throw new InvalidOperationException("YouTube provider configuration is missing.");
            }

            var envApiKey = Environment.GetEnvironmentVariable(provider.ApiKey);
            if (string.IsNullOrEmpty(envApiKey))
                throw new Exception($"Missing API key environment variable for {provider.ApiKey}");
            provider.ApiKey = envApiKey;

            var requestUri = $"search?q={Uri.EscapeDataString(movieName)}&part=id,snippet&type=video&key={provider.ApiKey}";
            var searchResponse = await httpClient.GetFromJsonAsync<YouTubeSearchResponse>(requestUri);

            if (searchResponse is null)
            {
                return new List<MovieVideo>();
            }

            return mapper.Map<List<MovieVideo>>(searchResponse);
        }
    }
}