using System;
using System.Threading.Tasks;

namespace Lx.Utilities.Contract.Caching
{
    public interface ICacheBase
    {
        T GetCachedItem<T>(string cacheKey);
        Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem, TimeSpan expiration);
    }
}