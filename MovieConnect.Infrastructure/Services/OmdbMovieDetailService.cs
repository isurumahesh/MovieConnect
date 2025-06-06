using AutoMapper;
using Microsoft.Extensions.Options;
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
            var apiKey = options.Value.MovieDetailProviders.First(a => a.Name == ProviderName).ApiKey;
            var searchResponse = await httpClient.GetFromJsonAsync<OmdbMovieResponse>($"?t={movieName}&apikey={apiKey}");

            if (searchResponse == null)
            {
                throw new Exception("Movie not found");
            }

            return mapper.Map<MovieDetail>(searchResponse);
        }
    }
}