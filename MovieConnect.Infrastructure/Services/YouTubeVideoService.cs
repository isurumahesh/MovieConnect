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
            var apiKey = options.Value.MovieVideoProviders.First(a => a.Name == ProviderName).ApiKey;
            var searchResponse = await httpClient.GetFromJsonAsync<YouTubeSearchResponse>($"search?q={movieName}&part=id,snippet&type=video&key={apiKey}");

            if (searchResponse is null)
            {
                return new List<MovieVideo>();
            }

            return mapper.Map<List<MovieVideo>>(searchResponse);
        }
    }
}