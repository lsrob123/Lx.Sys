using System;
using Autofac;
using Lx.Utilities.Contract.Web;
using Lx.Utilities.Contract.WindowsService;
using Lx.Utilities.Services.Config;
using Lx.Utilities.Services.IoC.AutoFac;
using Lx.Utilities.Services.Web;
using Microsoft.Owin.Hosting;

namespace Lx.Utilities.Services.WindowsService
{
    public abstract class ServiceManagerBase : IServiceManager
    {
        protected IDisposable WebAppInstance;

        /// <summary>
        ///     ServiceManagerBase
        /// </summary>
        /// <param name="explicitlyApplyGlobalPreConfiguration">
        ///     Defaulted to false as Autofac builder extension CallDefaultDependencyRegisters()
        ///     will also call Preconfigurator.Configure() to avoid null reference exceptions
        ///     when registering pre-configured instances.
        /// </param>
        /// <remarks>
        ///     Preconfigurator.Configure() is thread safe and idempotent
        /// </remarks>
        protected ServiceManagerBase(bool explicitlyApplyGlobalPreConfiguration = false)
        {
            if (explicitlyApplyGlobalPreConfiguration)
                Preconfigurator.Configure();
        }

        public abstract void StartService();

        public virtual void StopService()
        {
            WebAppInstance?.Dispose();
        }

        protected virtual string StartEndpointWithStaticFileFolders(params string[] staticFileRootFolders)
        {
            var container = new ContainerBuilder().StartEverything();
            var endPointBaseUri = container.Resolve<IWebEndpointSettings>().EndpointBaseUri;
            WebAppInstance = WebApp.Start(endPointBaseUri, app => app.UseEverything(container, staticFileRootFolders));
            Console.WriteLine("Server running on {0}", endPointBaseUri);

            return endPointBaseUri;
        }
    }
}