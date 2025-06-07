using MovieConnect.Core.Models;

namespace MovieConnect.Application.DTOs
{
    public record MovieResponseDTO
    {
        public MovieDetail? MovieDetail { get; init; }
        public List<MovieVideo> MovieVideos { get; init; } = new();
    }
}