using System;
using Autofac;
using Lx.Identity.Endpoint.Configurators;
using Lx.Utilities.Contract.Web;
using Lx.Utilities.Services.IoC.AutoFac;
using Lx.Utilities.Services.WindowsService;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace Lx.Identity.Endpoint
{
    public class ServiceManager : ServiceManagerBase
    {
        protected IDisposable WebAppInstance;

        public ServiceManager() : base(true)
        {
        }

        public override void StartService()
        {
            var container = new ContainerBuilder()
                .CallDefaultDependencyRegisters()
                .Build()
                .ClearCache()
                .SetAsGlobalDependencyResolver();

            var endpointBaseUri = container.Resolve<IWebEndpointSettings>().EndpointBaseUri;

            WebAppInstance = WebApp.Start(endpointBaseUri,
                app =>
                {
                    app.UseAutofacMiddleware(container);
                    app.UseCors(CorsOptions.AllowAll);

                    app.UseFileServer(StaticFileServerConfigurator.FileServerOptions());
                    app.UseIdentityServer(IdentityServerConfigurator.IdentityServerOptions(container));
                });

            Console.WriteLine("Server running on {0}", endpointBaseUri);
        }

        public override void StopService()
        {
            WebAppInstance.Dispose();
        }
    }
}