using MediatR;
using MovieConnect.Application.DTOs;

namespace MovieConnect.Application.Queries
{
    public record GetMoviesByName(string MovieName) : IRequest<MovieResponseDTO>;
}