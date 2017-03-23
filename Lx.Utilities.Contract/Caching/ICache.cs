using System;
using System.Threading.Tasks;

namespace Lx.Utilities.Contract.Caching {
    public interface ICache : ICacheBase, IDisposable {
        bool Exists(string cacheKey);
        Task<bool> RemoveCachedItemAsync(string cacheKey);
        Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem);
        void FlushDb();
    }
}