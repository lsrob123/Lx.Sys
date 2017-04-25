using System;
using System.Web.Http;
using Autofac;
using Lx.Utilities.Contract.Web;
using Lx.Utilities.Services.IoC.AutoFac;
using Lx.Utilities.Services.OWIN;
using Lx.Utilities.Services.SignalR;
using Lx.Utilities.Services.Web;
using Lx.Utilities.Services.WindowsService;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace Lx.Membership.Endpoint
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
                .RegisterWithSignalR()
                .RegisterWithWebApi()
                .Build()
                .StartBus()
                .InstantiateSignalRHubs()
                .ClearCache()
                .SetAsGlobalDependencyResolver();

            var endPointBaseUri = container.Resolve<IWebEndpointSettings>().EndpointBaseUri;

            WebAppInstance = WebApp.Start(endPointBaseUri,
                app =>
                {
                    app.UseAutofacMiddleware(container);

                    app.UseCors(CorsOptions.AllowAll);

                    app.UseFileServer(new FileServerOptions().WithFolderAndDefaultFile("Assets"));

                    app.MapSignalR(container.Resolve<ISignalRConfig>().VirtualFolder,
                        new HubConfiguration().WithAutofac(container));

                    app.UseWebApi(new HttpConfiguration()
                        .WithAutofac(container)
                        .WithDefaultSettings(app, container)
                        );
                });

            Console.WriteLine("Server running on {0}", endPointBaseUri);
        }

        public override void StopService()
        {
            WebAppInstance.Dispose();
        }
    }
}