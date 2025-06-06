namespace MovieConnect.Core.Constants
{
    public static class CacheConstants
    {
        public static string GetMovieCacheKey(string movieName) => $"MovieResponse-{movieName}";

        public const int CacheDurationInMinutes = 5;
    }
}