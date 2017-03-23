using System;
using Autofac;
using Lx.Utilities.Services.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Lx.Utilities.Services.IoC.AutoFac {
    public static class ContainerBuilderExtensionsForSignalR {
        public static ContainerBuilder RegisterSignalRHubWithInterface<THubInterface, THub>(
            this ContainerBuilder builder)
            where THub : Hub, THubInterface
            where THubInterface : IHub {
            builder.Register<THubInterface>(context => context.Resolve<THub>());
            return RegisterSignalRHub<THub>(builder);
        }

        public static ContainerBuilder RegisterSignalRHub<THub>(this ContainerBuilder builder)
            where THub : Hub {
            builder.RegisterType<THub>().ExternallyOwned().SingleInstance();
            return builder;
        }

        public static ContainerBuilder RegisterSignalRHub(this ContainerBuilder builder, Type hubType,
            bool ignoreHubTypeCheck = true) {
            if (!ignoreHubTypeCheck && !HubTypeHelper.HubTypeBase.IsAssignableFrom(hubType))
                throw new ArgumentOutOfRangeException(nameof(hubType));

            builder.RegisterType(hubType).ExternallyOwned().SingleInstance();
            return builder;
        }

        public static ContainerBuilder RegisterWithSignalR(this ContainerBuilder builder) {
            //builder.RegisterHubs(AssemblyHelper.GetReferencedAssemblies().ToArray()); // it creates a new instance on each request

            var hubTypes = HubTypeHelper.GetHubTypes();
            foreach (var hubType in hubTypes)
                RegisterSignalRHub(builder, hubType);

            return builder;
        }
    }
}