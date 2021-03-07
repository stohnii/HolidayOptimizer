using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace HolidayOptimizer.Cache
{
    public class InMemoryCache : ICacheProvider<Dictionary<string, string>>
    {
        private IMemoryCache _cache;

        public InMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddOrUpdate(Dictionary<string, string> data, string key)
        {
            _cache.Set(key, data);
        }

        public Dictionary<string, string> GetCached(string key)
        {
            return _cache.Get<Dictionary<string, string>>(key);
        }

        public bool KeyExists(string key)
        {
            return _cache.TryGetValue(key, out object val);
        }
    }
}
