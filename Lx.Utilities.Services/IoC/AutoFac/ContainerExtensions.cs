using Autofac;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.IoC;
using Lx.Utilities.Contracts.ServiceBus;
using Lx.Utilities.Services.ServiceBus.Nsb;
using Lx.Utilities.Services.SignalR;
using NServiceBus;

namespace Lx.Utilities.Services.IoC.AutoFac
{
    public static class ContainerExtensions
    {
        public static IContainer StartBus(this IContainer container, string licenseFilePath = null)
        {
            var busSettings = container.Resolve<IBusSettings>();
            Bus.Create(BusConfigurationHelper.GetBusConfiguration(busSettings, autofacContainer: container,
                    licenseFilePath: licenseFilePath))
                .Start();

            return container;
        }

        public static IContainer ClearCache(this IContainer container)
        {
            container.Resolve<ICacheFactory>().Execute(cache => cache.FlushDb());
            return container;
        }

        public static IContainer InstantiateSignalRHubs(this IContainer container)
        {
            var hubTypes = HubTypeHelper.GetHubTypes();
            foreach (var hubType in hubTypes)
                container.Resolve(hubType);

            return container;
        }

        public static IContainer SetAsGlobalDependencyResolver(this IContainer container)
        {
            GlobalDependencyResolver.Default.SetResolveAction(container.Resolve);
            return container;
        }
    }
}