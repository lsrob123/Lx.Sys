using System;
using System.Linq;
using Autofac;
using Lx.Utilities.Contracts.IoC;
using Lx.Utilities.Services.Config;
using Lx.Utilities.Services.Infrastructure;

namespace Lx.Utilities.Services.IoC.AutoFac
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder Call(this ContainerBuilder builder,
            Action<Action<Type, Type>, Action<Type, object>> executeRegistrations)
        {
            executeRegistrations(
                (tInterface, tImplementation) => builder.RegisterType(tImplementation).As(tInterface),
                (tInterface, instance) => builder.RegisterInstance(instance).As(tInterface)
            );
            return builder;
        }

        public static ContainerBuilder Call<TDependencyRegister>(this ContainerBuilder builder)
            where TDependencyRegister : DefaultDependencyRegisterBase, new()
        {
            var register = new TDependencyRegister();

            LinkRegisterToContainerBuilder(builder, register);
            return builder;
        }

        public static ContainerBuilder LinkRegisterToContainerBuilder(this ContainerBuilder builder,
            DefaultDependencyRegisterBase register)
        {
            register.SetActions(
                (tInterface, tImplementation, externallyOwned, singleInstance) =>
                {
                    if (externallyOwned)
                    {
                        if (singleInstance)
                            builder.RegisterType(tImplementation).ExternallyOwned().SingleInstance().As(tInterface);
                        else
                            builder.RegisterType(tImplementation).ExternallyOwned().As(tInterface);
                    }
                    else
                    {
                        if (singleInstance)
                            builder.RegisterType(tImplementation).SingleInstance().As(tInterface);
                        else
                            builder.RegisterType(tImplementation).As(tInterface);
                    }
                },
                (tInterface, instance, externallyOwned) =>
                {
                    if (externallyOwned)
                        builder.RegisterInstance(instance).ExternallyOwned().As(tInterface);
                    else
                        builder.RegisterInstance(instance).As(tInterface);
                }
            );

            register.AddRegistrations();

            return builder;
        }

        public static ContainerBuilder CallDefaultDependencyRegisters(this ContainerBuilder builder)
        {
            Preconfigurator.Configure();

            var dependencyRegisterTypeBase = typeof(DefaultDependencyRegisterBase);

            var dependencyRegisters = AssemblyHelper.GetReferencedAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => dependencyRegisterTypeBase.IsAssignableFrom(t) && !t.IsAbstract)
                .Select(t => (DefaultDependencyRegisterBase) Activator.CreateInstance(t))
                .ToList();

            foreach (var register in dependencyRegisters)
                LinkRegisterToContainerBuilder(builder, register);

            return builder;
        }

        public static IContainer StartForWindowsService(this ContainerBuilder builder, string licenseFilePath = null)
        {
            var container = new ContainerBuilder()
                .CallDefaultDependencyRegisters()
                .RegisterWithSignalR()
                .RegisterWithWebApi()
                .Build()
                .StartBus(licenseFilePath)
                .InstantiateSignalRHubs()
                .ClearCache()
                .SetAsGlobalDependencyResolver();

            return container;
        }

        public static IContainer StartNonWeb(this ContainerBuilder builder, string licenseFilePath = null)
        {
            var container = new ContainerBuilder()
                .CallDefaultDependencyRegisters()
                .Build()
                .StartBus(licenseFilePath)
                .ClearCache()
                .SetAsGlobalDependencyResolver();

            return container;
        }

        public static IContainer StartForMvcSite(this ContainerBuilder builder, string licenseFilePath = null)
        {
            var container = new ContainerBuilder()
                .CallDefaultDependencyRegisters()
                .RegisterWithSignalR()
                .RegisterWithWebApi()
                .RegisterWithMvc()
                .Build()
                .StartBus(licenseFilePath)
                .InstantiateSignalRHubs()
                .ClearCache()
                .SetAsGlobalDependencyResolver();

            return container;
        }

    }
}