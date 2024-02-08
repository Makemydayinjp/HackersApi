using Microsoft.Extensions.Caching.Memory;

namespace Santander.DeveloperTestAPI.Cache
{
    public class LocalCache : ILocalCache
    {
        private readonly IMemoryCache _memoryCache;

        public LocalCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add<TOut>(int key, TOut? outValue, TimeSpan? timeSpan = null) where TOut : class, new()
        {
            var keepFor = timeSpan ?? TimeSpan.FromMinutes(10);
            _memoryCache.Set(key, outValue, keepFor);
        }

        public bool TryGet<TOut>(int key, out TOut? outValue) where TOut : class, new()
        {
            return _memoryCache.TryGetValue(key, out outValue);
        }

    }
}
