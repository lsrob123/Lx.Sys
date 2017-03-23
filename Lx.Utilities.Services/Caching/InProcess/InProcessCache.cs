using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Lx.Utilities.Contract.Caching;

namespace Lx.Utilities.Services.Caching.InProcess {
    public class InProcessCache : IInProcessCache {
        protected readonly ObjectCache CacheInstance = MemoryCache.Default;

        public T GetCachedItem<T>(string cacheKey) {
            var item = (T) CacheInstance.Get(cacheKey);
            return item;
        }

        public async Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem, TimeSpan expiration) {
            var result = SetCachedItem(cacheKey, cachedItem, expiration);
            return await Task.FromResult(result);
        }

        public bool SetCachedItem<T>(string cacheKey, T cachedItem, TimeSpan expiration) {
            if (cachedItem == null)
                return false;

            var currentTime = DateTime.UtcNow;
            var expiryTime = currentTime.Add(expiration);
            CacheInstance.Set(cacheKey, cachedItem, expiryTime);
            return true;
        }
    }
}