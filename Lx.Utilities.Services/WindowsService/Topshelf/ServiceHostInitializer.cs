using System;
using System.Reflection;
using Topshelf;

namespace Lx.Utilities.Services.WindowsService.Topshelf {
    public class ServiceHostInitializer<TServiceManager>
        where TServiceManager : ServiceManagerBase, new() {
        public ServiceHostInitializer(string serviceName = null, Action<int> handleExitOnError = null,
            bool runAsNetworkService = false) {
            if (string.IsNullOrWhiteSpace(serviceName))
                serviceName = Assembly.GetEntryAssembly().GetName().Name;

            Host = HostFactory.New(
                configurator => {
                    configurator.Service<TServiceManager>(service => {
                        service.ConstructUsing(s => new TServiceManager());
                        service.WhenStarted(start => start.StartService());
                        service.WhenStopped(stop => stop.StopService());
                    });

                    if (runAsNetworkService)
                        configurator.RunAsNetworkService();
                    else
                        configurator.RunAsLocalSystem();

                    //configurator.SetDescription("Sample windows service description");
                    configurator.SetDisplayName(serviceName);
                    configurator.SetServiceName(serviceName);
                    configurator.EnableServiceRecovery(action => action.RestartService(1));
                });

            var exitCode = Host.Run();
            if (exitCode != TopshelfExitCode.Ok)
                handleExitOnError?.Invoke((int) exitCode);
        }

        public Host Host { get; protected set; }
    }
}