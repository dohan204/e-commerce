using System.Collections;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace infrastructure.cache.helpers
{
    public abstract class Helpers
    {
        // private readonly IConfiguration configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly int _absoluteExpiration;
        private readonly int _slidingExpiration;
        // private static readonly HashSet<string> _cacheKey = new();
        private static readonly ConcurrentDictionary<string, byte> _cache = new ();
        public Helpers(IMemoryCache memoryCache, IConfiguration configuration)
        {
            this._memoryCache = memoryCache;
            _absoluteExpiration = configuration.GetValue<int>("Caches:AbsoluteExpiration");
            _slidingExpiration = configuration.GetValue<int>("Caches:SlidingExpiration");
        }
        protected async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory)
        {
            if (!_memoryCache.TryGetValue(key, out T? value))
            {
                value = await factory();

                if (value is not null)
                {
                    var options = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_absoluteExpiration),
                        SlidingExpiration = TimeSpan.FromMinutes(_slidingExpiration),
                        Priority = CacheItemPriority.High
                    };

                    _memoryCache.Set(key, value, options);

                    _cache.TryAdd(key, 0);
                }
            }
            return value;
        }

        protected void RemoveCache(string key)
        {
            _memoryCache.Remove(key);
            _cache.TryRemove(key, out _);
        }
        protected void RemoveByPrefix(string prefix)
        {
            var keyToRemove = _cache.Keys
            .Where(e => e.StartsWith(prefix))
            .ToList();

            foreach(var key in keyToRemove)
            {
                _memoryCache.Remove(key);
                _cache.TryRemove(key, out _);
            }
        }
    }
}