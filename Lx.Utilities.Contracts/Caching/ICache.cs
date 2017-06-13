using System;
using System.Threading.Tasks;

namespace Lx.Utilities.Contracts.Caching
{
    public interface ICache : ICacheBase, IDisposable
    {
        object GetCachedItem(string cacheKey, Type type);
        bool Exists(string cacheKey);
        Task<bool> RemoveCachedItemAsync(string cacheKey);
        Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem);
        void FlushDb();
    }
}