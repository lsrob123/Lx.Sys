using System;
using Autofac;
using Lx.Utilities.Contract.Web;
using Lx.Utilities.Services.Authentication;
using Lx.Utilities.Services.IoC.AutoFac;
using Lx.Utilities.Services.OWIN;
using Lx.Utilities.Services.WindowsService;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
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
                .StartBus()
                .ClearCache()
                .SetAsGlobalDependencyResolver();

            var endpointBaseUri = container.Resolve<IWebEndpointSettings>().EndpointBaseUri;

            WebAppInstance = WebApp.Start(endpointBaseUri,
                app =>
                {
                    app.UseAutofacMiddleware(container);
                    app.UseCors(CorsOptions.AllowAll);

                    app.UseFileServer(new FileServerOptions().WithFolderAndDefaultFile("Assets"));
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