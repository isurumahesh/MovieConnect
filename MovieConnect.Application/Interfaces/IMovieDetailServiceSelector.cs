using MovieConnect.Core.Interfaces;

namespace MovieConnect.Application.Interfaces
{
    public interface IMovieDetailServiceSelector
    {
        IMovieDetailService GetService();
    }
}