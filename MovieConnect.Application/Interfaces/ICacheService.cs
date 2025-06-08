namespace MovieConnect.Application.Interfaces
{
    public interface ICacheService
    {
        T? Get<T>(string key);

        void Set<T>(string key, T value, TimeSpan duration, int size = 1);

        void Remove(string key);
    }
}