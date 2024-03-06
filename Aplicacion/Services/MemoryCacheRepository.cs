using Microsoft.Extensions.Caching.Memory;

namespace Tekton.Application.Services
{
	public class MemoryCacheRepository<T> : ICacheRepository<T>
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get(string key) => _memoryCache.Get<T>(key);
        

        public void Set(string key, T item, TimeSpan? expiry = null)
        {
            var options = new MemoryCacheEntryOptions();
            if (expiry.HasValue)
            {
                options.SetAbsoluteExpiration(expiry.Value);
            }
            _memoryCache.Set(key, item, options);
        }

        public void Remove(string key)
        { 
            _memoryCache.Remove(key);
        }

        public T TryGetValue(string key)
        {
            _memoryCache.TryGetValue(key, out T item);
            return item;
        }
    }
}

