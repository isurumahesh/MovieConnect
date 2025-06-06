namespace MovieConnect.Application.Interfaces
{
    public interface IMovieProviderConfiguration
    {
        string GetActiveMovieDetailProvider();

        string GetActiveMovieVideoProvider();
    }
}