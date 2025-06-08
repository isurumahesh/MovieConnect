using MediatR;
using Microsoft.Extensions.Logging;
using MovieConnect.Application.DTOs;
using MovieConnect.Application.Interfaces;
using MovieConnect.Core.Constants;

namespace MovieConnect.Application.Queries
{
    public class GetMoviesByNameQueryHandler(IMovieDetailServiceSelector movieDetailServiceSelector, IMovieVideoServiceSelector movieVideoServiceSelector,
        ICacheService cacheService, ILogger<GetMoviesByNameQueryHandler> logger) : IRequestHandler<GetMoviesByName, MovieResponseDTO>
    {
        public async Task<MovieResponseDTO> Handle(GetMoviesByName request, CancellationToken cancellationToken)
        {
            var cacheKey = CacheConstants.GetMovieCacheKey(request.MovieName);

            var movieResponse = cacheService.Get<MovieResponseDTO>(cacheKey);
            if (movieResponse is not null)
            {
                logger.LogInformation($"{request.MovieName} is retrieved from cache");
                return movieResponse;
            }

            var movieDetailService = movieDetailServiceSelector.GetService();
            var movieVideoService = movieVideoServiceSelector.GetService();

            var movieDetailsTask = movieDetailService.GetMovieDetailsAsync(request.MovieName);
            var movieVideosTask = movieVideoService.GetMovieVideosAsync(request.MovieName);

            await Task.WhenAll(movieDetailsTask, movieVideosTask);

            var movieDetail = await movieDetailsTask;
            var movieVideos = await movieVideosTask;

            movieResponse = new MovieResponseDTO
            {
                MovieDetail = movieDetail,
                MovieVideos = movieVideos
            };

            cacheService.Set(cacheKey, movieResponse, TimeSpan.FromMinutes(CacheConstants.CacheDurationInMinutes));

            return movieResponse;
        }
    }
}