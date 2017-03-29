using System;
using System.Configuration;
using Lx.Utilities.Contract.ServiceBus;
using Lx.Utilities.Services.Config;

namespace Lx.Utilities.Services.ServiceBus.Nsb {
    internal class NsbSettingsInConfigFile {
        public string PersistenceStoreConnectionString { get; set; }
        public string MqConnectionString { get; set; }
        public string LogFolderRelativePath { get; set; }
        public bool? PurgeOnStartup { get; set; }
    }

    public abstract class NsbBusSettingsBase : IBusSettings {
        protected NsbBusSettingsBase(string endpointName, string dbConnectionString = "nservicebus",
            string mqConnectionString = null, bool purgeOnStartup = false, Type serializerType = null,
            string logFolderRelativePath = null) {
            var settingsInConfigFile = new NsbSettingsInConfigFile {
                PersistenceStoreConnectionString =
                    ConfigurationManager.ConnectionStrings[dbConnectionString ?? "nservicebus"].ConnectionString,
                MqConnectionString = this.AppSettingStringValue(x => x.MqConnectionString),
                LogFolderRelativePath = this.AppSettingStringValue(x => x.LogFolderRelativePath),
                PurgeOnStartup = this.AppSettingNullableBooleanValue(x => x.PurgeOnStartup)
            };

            EndpointName = endpointName;
            PersistenceStoreConnectionString = settingsInConfigFile.PersistenceStoreConnectionString;
            MqConnectionString = mqConnectionString ?? settingsInConfigFile.MqConnectionString ?? "host=localhost";

            PurgeOnStartup = settingsInConfigFile.PurgeOnStartup ?? purgeOnStartup;
            // Highly likely it was not set in config file

            SerializerType = serializerType;
            LogFolderRelativePath = logFolderRelativePath ?? settingsInConfigFile.LogFolderRelativePath ?? "_logs";
        }

        public string EndpointName { get; protected set; }

        public string MqConnectionString { get; protected set; }

        public Type SerializerType { get; protected set; }
        public string PersistenceStoreConnectionString { get; protected set; }
        public string LogFolderRelativePath { get; protected set; }
        public bool PurgeOnStartup { get; protected set; }

        public virtual string GetSendEndpoint<TBusCommand>(TBusCommand command = default(TBusCommand))
            where TBusCommand : IBusCommand {
            return EndpointName;
        }
    }
}