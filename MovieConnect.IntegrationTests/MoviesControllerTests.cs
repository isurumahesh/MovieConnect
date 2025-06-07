using Microsoft.AspNetCore.Mvc.Testing;
using MovieConnect.Application.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace MovieConnect.IntegrationTests
{
    public class MoviesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MoviesControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            var apiKey = Environment.GetEnvironmentVariable("MOVIE_CONNECT_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("MOVIE_CONNECT_API_KEY environment variable is not set.");
            }
            _client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        }

        [Fact]
        public async Task Get_ReturnsBadRequest_WhenMovieNameIsNullOrEmpty()
        {
            // Arrange
            var url = "/api/movies?movieName=";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Get_ReturnsOkAndMovieResponse_WhenMovieNameIsValid()
        {
            // Arrange
            var url = "/api/movies?movieName=Inception";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var movieResponse = await response.Content.ReadFromJsonAsync<MovieResponseDTO>();
            Assert.NotNull(movieResponse);
        }
    }
}