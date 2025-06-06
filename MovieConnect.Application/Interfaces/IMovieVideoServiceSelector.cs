using MovieConnect.Core.Interfaces;

namespace MovieConnect.Application.Interfaces
{
    public interface IMovieVideoServiceSelector
    {
        IMovieVideoService GetService();
    }
}