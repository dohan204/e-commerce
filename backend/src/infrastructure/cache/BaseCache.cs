using System.Collections.Concurrent;
// using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace infrastructure.cache
{
    public class BaseCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, bool> _cacheKeys;
        public BaseCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheKeys = new ConcurrentDictionary<string, bool>();
        }

        public void Set<T>(string key, T value, MemoryCacheEntryOptions options)
        {
            _memoryCache.Set(key, value, options);
            _cacheKeys.TryAdd(key, true);
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            if(_memoryCache.TryGetValue(key, out value))
            {
                return true;
            }

            // neues kh thay cache, remove khoi dictionnary
            _cacheKeys.TryRemove(key, out _);
            value = default;
            return false;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
            _cacheKeys.TryRemove(key, out _);

        }

        public List<string> GetAllKeys()
        {
            return _cacheKeys.Keys.ToList();
        }

        public void Clear()
        {
            foreach(var item in _cacheKeys.Keys)
            {
                _memoryCache.Remove(item);
            }

            _cacheKeys.Clear();
        }
    }
}