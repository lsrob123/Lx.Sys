using System;
using Lx.Utilities.Contracts.Caching;

namespace Lx.Utilities.Services.Caching.Redis
{
    public class CacheFactory : ICacheFactory
    {
        public ICacheWithHashes NewDisposableCache()
        {
            return new Cache();
        }

        public void Execute(Action<ICacheWithHashes> action)
        {
            using (var cache = NewDisposableCache())
            {
                action(cache);
            }
        }

        public void Set(string cacheKey, object cachedItem, TimeSpan? expiration = null)
        {
            Execute(cache => Set(cache, cacheKey, cachedItem, expiration));
        }

        public T Get<T>(Func<ICacheWithHashes, T> action)
        {
            var value = default(T);
            Execute(cache => value = action(cache));

            return value;
        }

        protected virtual void Set(ICacheWithHashes cache, string cacheKey, object cachedItem, TimeSpan? expiration)
        {
            if (expiration.HasValue)
                cache.SetCachedItemAsync(cacheKey, cachedItem, expiration.Value).Wait();
            else
                cache.SetCachedItemAsync(cacheKey, cachedItem).Wait();
        }
    }
}