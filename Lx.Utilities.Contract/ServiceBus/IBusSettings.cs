using System;

namespace Lx.Utilities.Contract.ServiceBus {
    public interface IBusSettings {
        string EndpointName { get; }
        string MqConnectionString { get; }
        Type SerializerType { get; }
        string PersistenceStoreConnectionString { get; }
        string LogFolderRelativePath { get; }
        bool PurgeOnStartup { get; }
        string GetSendEndpoint<TBusCommand>(TBusCommand command = default(TBusCommand)) where TBusCommand : IBusCommand;
    }
}