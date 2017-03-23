using System;
using System.IO;
using Autofac;
using Lx.Utilities.Contract.ServiceBus;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;
using NServiceBus.Newtonsoft.Json;
using NServiceBus.Persistence;

namespace Lx.Utilities.Services.ServiceBus.NSB {
    public class BusConfigurationHelper {
        protected static readonly Type BusCommandType = typeof(IBusCommand);
        protected static readonly Type BusEventType = typeof(IBusEvent);
        protected static readonly Type BusMessageType = typeof(IBusMessage);

        public static BusConfiguration GetBusConfiguration(IBusSettings settings,
            bool useDefaultAutofacContainer = false, IContainer autofacContainer = null,
            Action<BusConfiguration> registerIocContainer = null) {
            var busConfiguration = new BusConfiguration();

            busConfiguration.EndpointName(settings.EndpointName);

            //busConfiguration.UseSerialization(settings.SerializerType ?? typeof(JsonSerializer));
            busConfiguration.UseSerialization(settings.SerializerType ?? typeof(NewtonsoftSerializer));

            var transport = busConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString(settings.MqConnectionString);

            busConfiguration.LicensePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "License.xml"));

            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<NHibernatePersistence>()
                .ConnectionString(settings.PersistenceStoreConnectionString);

            busConfiguration.Conventions().DefiningCommandsAs(commandType =>
                    (commandType.Namespace != null) && BusCommandType.IsAssignableFrom(commandType));
            busConfiguration.Conventions().DefiningEventsAs(
                eventType => (eventType.Namespace != null) && BusEventType.IsAssignableFrom(eventType));
            busConfiguration.Conventions().DefiningMessagesAs(messageType =>
                    (messageType.Namespace != null) && BusMessageType.IsAssignableFrom(messageType));

            busConfiguration.PurgeOnStartup(settings.PurgeOnStartup);

            busConfiguration.DisableFeature<SecondLevelRetries>();

            if (registerIocContainer == null) {
                if (useDefaultAutofacContainer)
                    busConfiguration.RegisterWithAutofac();
                else if (autofacContainer != null)
                    busConfiguration.RegisterWithAutofac(autofacContainer);
            } else {
                registerIocContainer(busConfiguration);
            }

            var loggingFactory = LogManager.Use<DefaultFactory>();

            var logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settings.LogFolderRelativePath);
            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

            loggingFactory.Directory(logFolder);
            loggingFactory.Level(LogLevel.Debug);

            return busConfiguration;
        }
    }
}