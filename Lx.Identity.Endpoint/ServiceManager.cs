using System;
using Autofac;
using Lx.Utilities.Contract.Web;
using Lx.Utilities.Services.IoC.AutoFac;
using Lx.Utilities.Services.Web;
using Lx.Utilities.Services.WindowsService;
using Microsoft.Owin.Hosting;

namespace Lx.Identity.Endpoint
{
    public class ServiceManager : ServiceManagerBase
    {
        public ServiceManager() : base(true)
        {
        }

        public override void StartService()
        {
            var container = new ContainerBuilder().StartNonWeb();
            var endpointBaseUri = container.Resolve<IWebEndpointSettings>().EndpointBaseUri;
            WebAppInstance = WebApp.Start(endpointBaseUri, app => app.WithIdentityServer(container));

            Console.WriteLine("Server running on {0}", endpointBaseUri);
        }

        public override void StopService()
        {
            WebAppInstance.Dispose();
        }
    }
}