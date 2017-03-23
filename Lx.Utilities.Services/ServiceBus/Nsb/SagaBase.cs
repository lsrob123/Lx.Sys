using System;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Serialization;
using NServiceBus.Saga;

namespace Lx.Utilities.Services.ServiceBus.Nsb {
    public abstract class SagaBase<TSagaData> : Saga<TSagaData>, IHasInstanceKey
        where TSagaData : IContainSagaData, new() {
        protected readonly ICacheFactory CacheFactory;
        protected readonly ILogger Logger;
        protected readonly IMappingService MappingService;
        protected readonly ISerializer Serializer;

        protected SagaDataCache DataCacheInstance;

        protected volatile bool SagaCompleted, TimeoutRequested;

        protected SagaBase(ICacheFactory cacheFactory, ISerializer serializer, IMappingService mappingService,
            ILogger logger) {
            CacheFactory = cacheFactory;
            Serializer = serializer;
            MappingService = mappingService;
            Logger = logger;
        }

        protected SagaDataCache DataCache
            => DataCacheInstance ?? (DataCacheInstance = new SagaDataCache(CacheFactory, Serializer, Data.Id));

        public Guid InstanceKey => Data.Id;

        protected new void RequestTimeout<TTimeoutMessageType>(DateTime at) where TTimeoutMessageType : new() {
            if (TimeoutRequested)
                return;

            TimeoutRequested = true;
            base.RequestTimeout<TTimeoutMessageType>(at);
        }

        protected new void RequestTimeout<TTimeoutMessageType>(TimeSpan within) where TTimeoutMessageType : new() {
            if (TimeoutRequested)
                return;

            TimeoutRequested = true;
            base.RequestTimeout<TTimeoutMessageType>(within);
        }

        protected new void RequestTimeout<TTimeoutMessageType>(DateTime at, TTimeoutMessageType timeoutMessage) {
            if (TimeoutRequested)
                return;

            TimeoutRequested = true;
            base.RequestTimeout(at, timeoutMessage);
        }

        protected new void RequestTimeout<TTimeoutMessageType>(TimeSpan within, TTimeoutMessageType timeoutMessage) {
            if (TimeoutRequested)
                return;

            TimeoutRequested = true;
            base.RequestTimeout(within, timeoutMessage);
        }

        ~SagaBase() {
            SagaCompleted = true;
        }

        protected override void MarkAsComplete() {
            SagaCompleted = true;
            DataCache.RemoveAll();
            DataCache.Dispose();
            base.MarkAsComplete();
        }
    }
}