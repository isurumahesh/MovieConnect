using AutoMapper;
using Microsoft.Extensions.Options;
using MovieConnect.Application.Exceptions;
using MovieConnect.Core.Interfaces;
using MovieConnect.Core.Models;
using MovieConnect.Infrastructure.ApiResponse;
using MovieConnect.Infrastructure.Configurations;
using System.Net.Http.Json;

namespace MovieConnect.Infrastructure.Services
{
    public class OmdbMovieDetailService(HttpClient httpClient, IMapper mapper, IOptions<MovieProviderOptions> options) : IMovieDetailService
    {
        public string ProviderName => "omdb";

        public async Task<MovieDetail> GetMovieDetailsAsync(string movieName)
        {
            if (string.IsNullOrWhiteSpace(movieName))
                throw new ArgumentException("Movie name must be provided.", nameof(movieName));
         
            var provider = options.Value.MovieDetailProviders.FirstOrDefault(a => a.Name == ProviderName);

            if (provider is null || string.IsNullOrEmpty(provider.ApiKey))
            {
                throw new InvalidOperationException("Omdb provider configuration is missing.");
            }

            var envApiKey = Environment.GetEnvironmentVariable(provider.ApiKey);
            if (string.IsNullOrEmpty(envApiKey))
                throw new Exception($"Missing API key environment variable for {provider.ApiKey}");
            provider.ApiKey = envApiKey;

            var requestUri = $"?t={Uri.EscapeDataString(movieName)}&apikey={provider.ApiKey}";
            var searchResponse = await httpClient.GetFromJsonAsync<OmdbMovieResponse>(requestUri);

            if (searchResponse == null || searchResponse.Response=="False")
            {
                throw new MovieNotFoundException($"Movie '{movieName}' not found.");
            }

            return mapper.Map<MovieDetail>(searchResponse);
        }
    }
}