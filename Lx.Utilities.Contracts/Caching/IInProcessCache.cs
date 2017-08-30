using System;

namespace Lx.Utilities.Contracts.Caching
{
    public interface IInProcessCache : ICacheBase
    {
        bool SetCachedItem<T>(string cacheKey, T cachedItem, TimeSpan expiration);
    }
}