using MovieConnect.Core.Models;

namespace MovieConnect.Core.Interfaces
{
    public interface IMovieVideoService
    {
        string ProviderName { get; }

        Task<List<MovieVideo>> GetMovieVideosAsync(string movieName);
    }
}