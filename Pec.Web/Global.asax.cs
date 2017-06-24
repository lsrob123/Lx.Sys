using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Lx.Utilities.Services.Config;
using Lx.Utilities.Services.IoC.AutoFac;

namespace Pec.Web
{
    public class Global : HttpApplication
    {
        private static readonly IContainer Container;

        static Global()
        {
            Preconfigurator.Configure();
            Container = new ContainerBuilder().StartForMvcSite();
        }

        private void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}