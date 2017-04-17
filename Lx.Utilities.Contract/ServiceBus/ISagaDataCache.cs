using System;

namespace Lx.Utilities.Contract.ServiceBus
{
    public interface ISagaDataCache : IDisposable
    {
        Guid SagaId { get; }
        void SetItem<T>(T data, string key = null);
        T GetItem<T>(string key = null);
        void RemoveAll();
    }
}