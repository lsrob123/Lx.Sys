using System;

namespace Lx.Utilities.Contracts.ServiceBus
{
    public interface ISagaDataCache : IDisposable
    {
        Guid SagaId { get; }
        void SetItem<T>(T data, string key = null);
        T GetItem<T>(string key = null);
        void RemoveAll();
    }
}