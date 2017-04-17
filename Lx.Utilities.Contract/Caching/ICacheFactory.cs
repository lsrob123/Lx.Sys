using System;

namespace Lx.Utilities.Contract.Caching
{
    public interface ICacheFactory
    {
        ICacheWithHashes NewDisposableCache();
        void Execute(Action<ICacheWithHashes> action);
        void Set(string cacheKey, object cachedItem, TimeSpan? expiration = null);
        T Get<T>(Func<ICacheWithHashes, T> action);
    }
}