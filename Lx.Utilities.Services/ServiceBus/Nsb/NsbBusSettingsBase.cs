using System;
using System.Configuration;
using Lx.Utilities.Contracts.ServiceBus;
using Lx.Utilities.Services.Config;

namespace Lx.Utilities.Services.ServiceBus.Nsb
{
    internal class NsbSettingsInConfigFile
    {
        public string PersistenceStoreConnectionString { get; set; }
        public string MqConnectionString { get; set; }
        public string LogFolderRelativePath { get; set; }
        public bool? PurgeOnStartup { get; set; }
    }

    public abstract class NsbBusSettingsBase : IBusSettings
    {
        public abstract string EndpointName { get; }

        public virtual string MqConnectionString
            => this.AppSettingStringValue(x => x.MqConnectionString) ?? "host=localhost";

        public virtual Type SerializerType => null;

        public virtual string PersistenceStoreConnectionString
            => ConfigurationManager.ConnectionStrings["nservicebus"].ConnectionString;

        public virtual string LogFolderRelativePath
            => this.AppSettingStringValue(x => x.LogFolderRelativePath) ?? "_logs";

        public virtual bool PurgeOnStartup => this.AppSettingNullableBooleanValue(x => x.PurgeOnStartup) ?? false;

        public virtual string GetSendEndpoint<TBusCommand>(TBusCommand command = default(TBusCommand))
            where TBusCommand : IBusCommand
        {
            return EndpointName;
        }
    }
}