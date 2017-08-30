using System;
using System.Threading.Tasks;

namespace Lx.Utilities.Contracts.Caching
{
    public interface ICacheBase
    {
        T GetCachedItem<T>(string cacheKey);
        Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem, TimeSpan expiration);
    }
}