using System;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Services.ServiceBus {
    public class SagaDataCache : ISagaDataCache {
        protected readonly ICacheWithHashes Cache;
        protected readonly string CacheHashKey;
        protected readonly ISerializer Serializer;

        public SagaDataCache(ICacheFactory cacheFactory, ISerializer serializer, Guid sagaId) {
            Cache = cacheFactory.NewDisposableCache();
            Serializer = serializer;
            SagaId = sagaId;
            CacheHashKey = nameof(SagaDataCache) + sagaId;
        }

        public Guid SagaId { get; protected set; }

        public void SetItem<T>(T data, string key = null) {
            if (data == null)
                return;

            var cacheKey = $"{typeof(T).Name}{key ?? string.Empty}";

            var cachedData = Serializer.Serialize(data);

            Cache.HashSetAsync(CacheHashKey, cacheKey, cachedData).Wait();
        }

        public T GetItem<T>(string key = null) {
            var cacheKey = $"{typeof(T).Name}{key ?? string.Empty}";

            var cachedData = Cache.HashGet(CacheHashKey, cacheKey);
            if (string.IsNullOrWhiteSpace(cachedData))
                return default(T);

            var data = Serializer.Deserialize<T>(cachedData);

            return data;
        }

        public void RemoveAll() {
            Cache.HashDeleteAsync(CacheHashKey).Wait();
        }

        public void Dispose() {
            Cache.Dispose();
        }
    }
}