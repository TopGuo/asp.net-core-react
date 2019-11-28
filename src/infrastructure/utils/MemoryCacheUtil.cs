using System;
using infrastructure.extensions;
using Microsoft.Extensions.Caching.Memory;

namespace infrastructure.utils
{
    public class MemoryCacheUtil
    {
        private static IMemoryCache cache = ServiceExtension.Get<IMemoryCache>();
        public static object Get(string key)
        {
            return cache.GetOrCreate<object>(key, factory => factory.Value);
        }
        public static void Set(string key, object value, double time = 1)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(time));
            cache.Set<object>(key, value, cacheEntryOptions);
        }
    }
}