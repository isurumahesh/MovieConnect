using MovieConnect.Core.Models;

namespace MovieConnect.Application.DTOs
{
    public class MovieResponseDTO
    {
        public MovieDetail? MovieDetail { get; set; }
        public List<MovieVideo> MovieVideos { get; set; } = new List<MovieVideo>();
    }
}