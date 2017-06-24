using System.Web.Mvc;
using System.Web.Routing;

namespace Pec.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Home",
                "home",
                new { controller = "Home", action = "Home" }
            );

            routes.MapRoute(
                "Login",
                "login",
                new { controller = "Home", action = "Login"}
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}