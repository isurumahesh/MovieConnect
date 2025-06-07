using Moq;
using MovieConnect.Application.DTOs;
using MovieConnect.Application.Interfaces;
using MovieConnect.Application.Queries;
using MovieConnect.Core.Constants;
using MovieConnect.Core.Interfaces;
using MovieConnect.Core.Models;

namespace MovieConnect.UnitTests.Application
{
    public class GetMoviesByNameQueryHandlerTests
    {
        private readonly Mock<IMovieDetailServiceSelector> _movieDetailServiceSelectorMock;
        private readonly Mock<IMovieVideoServiceSelector> _movieVideoServiceSelectorMock;
        private readonly Mock<ICacheService> _cacheServiceMock;

        private readonly GetMoviesByNameQueryHandler _handler;

        public GetMoviesByNameQueryHandlerTests()
        {
            _movieDetailServiceSelectorMock = new Mock<IMovieDetailServiceSelector>();
            _movieVideoServiceSelectorMock = new Mock<IMovieVideoServiceSelector>();
            _cacheServiceMock = new Mock<ICacheService>();

            _handler = new GetMoviesByNameQueryHandler(
                _movieDetailServiceSelectorMock.Object,
                _movieVideoServiceSelectorMock.Object,
                _cacheServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFromCache_IfCacheExists()
        {
            // Arrange
            var movieName = "Inception";
            var cacheKey = CacheConstants.GetMovieCacheKey(movieName);

            var cachedResponse = new MovieResponseDTO
            {
                MovieDetail = new MovieDetail { Title = movieName },
                MovieVideos = new List<MovieVideo>()
            };

            _cacheServiceMock.Setup(c => c.Get<MovieResponseDTO>(cacheKey))
                             .Returns(cachedResponse);

            var query = new GetMoviesByName(movieName);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieName, result.MovieDetail!.Title);
            _cacheServiceMock.Verify(c => c.Get<MovieResponseDTO>(cacheKey), Times.Once);

            _movieDetailServiceSelectorMock.Verify(m => m.GetService(), Times.Never);
            _movieVideoServiceSelectorMock.Verify(m => m.GetService(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldFetchFromServicesAndCache_WhenCacheMiss()
        {
            // Arrange
            var movieName = "Interstellar";
            var cacheKey = CacheConstants.GetMovieCacheKey(movieName);

            _cacheServiceMock.Setup(c => c.Get<MovieResponseDTO>(cacheKey))
                             .Returns((MovieResponseDTO)null);

            var movieDetailServiceMock = new Mock<IMovieDetailService>();
            var movieVideoServiceMock = new Mock<IMovieVideoService>();

            var movieDetail = new MovieDetail { Title = movieName };
            var movieVideos = new List<MovieVideo>
            {
                new MovieVideo { VideoUrl = new Uri("http://video1") },
                new MovieVideo { VideoUrl = new Uri("http://video2") }
            };

            movieDetailServiceMock.Setup(s => s.GetMovieDetailsAsync(movieName))
                                  .ReturnsAsync(movieDetail);

            movieVideoServiceMock.Setup(s => s.GetMovieVideosAsync(movieName))
                                 .ReturnsAsync(movieVideos);

            _movieDetailServiceSelectorMock.Setup(s => s.GetService())
                                           .Returns(movieDetailServiceMock.Object);

            _movieVideoServiceSelectorMock.Setup(s => s.GetService())
                                         .Returns(movieVideoServiceMock.Object);

            _cacheServiceMock.Setup(c => c.Set(cacheKey, It.IsAny<MovieResponseDTO>(), It.IsAny<TimeSpan>()))
                             .Verifiable();

            var query = new GetMoviesByName(movieName);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieName, result.MovieDetail!.Title);
            Assert.Equal(movieVideos.Count, result.MovieVideos.Count);

            _cacheServiceMock.Verify(c => c.Get<MovieResponseDTO>(cacheKey), Times.Once);
            _movieDetailServiceSelectorMock.Verify(s => s.GetService(), Times.Once);
            _movieVideoServiceSelectorMock.Verify(s => s.GetService(), Times.Once);
            _cacheServiceMock.Verify(c => c.Set(cacheKey, It.IsAny<MovieResponseDTO>(), It.IsAny<TimeSpan>()), Times.Once);
        }
    }
}