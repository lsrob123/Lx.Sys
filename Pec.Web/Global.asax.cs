using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Lx.Utilities.Services.IoC.AutoFac;

namespace Pec.Web
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = new ContainerBuilder().StartForMvcSite();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}