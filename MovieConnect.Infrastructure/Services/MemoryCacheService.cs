using Microsoft.Extensions.Caching.Memory;
using MovieConnect.Application.Interfaces;

namespace MovieConnect.Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T? Get<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T value);
            return value;
        }

        public void Set<T>(string key, T value, TimeSpan duration, int size = 1)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = duration,
                Size = size
            };

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}