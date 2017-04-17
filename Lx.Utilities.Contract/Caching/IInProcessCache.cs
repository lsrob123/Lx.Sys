using System;

namespace Lx.Utilities.Contract.Caching
{
    public interface IInProcessCache : ICacheBase
    {
        bool SetCachedItem<T>(string cacheKey, T cachedItem, TimeSpan expiration);
    }
}