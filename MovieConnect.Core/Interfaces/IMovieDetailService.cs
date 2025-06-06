using MovieConnect.Core.Models;

namespace MovieConnect.Core.Interfaces
{
    public interface IMovieDetailService
    {
        string ProviderName { get; }

        Task<MovieDetail> GetMovieDetailsAsync(string movieName);
    }
}