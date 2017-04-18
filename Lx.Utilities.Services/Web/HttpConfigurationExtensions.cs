using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Filters;
using Autofac;
using Autofac.Integration.WebApi;
using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Services.Authentication;
using Owin;

namespace Lx.Utilities.Services.Web
{
    public static class HttpConfigurationExtensions
    {
        public static HttpConfiguration WithAutofac(this HttpConfiguration config, IContainer autofacContainer)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(autofacContainer);
            return config;
        }

        public static HttpConfiguration WithDefaultSettings(this HttpConfiguration config, IAppBuilder app,
            IContainer autofacContainer, IEnumerable<IFilter> filters = null, bool mapAttributeRoutes = true)
        {
            if (mapAttributeRoutes)
                config.MapHttpAttributeRoutes();

            config.Filters.AddRange(filters ?? new List<IFilter>());

            app.Use(typeof(OAuthAuthenticationMiddleware), autofacContainer.Resolve<ILogger>(),
                autofacContainer.Resolve<IOAuthClientSettings>(), autofacContainer.Resolve<IClaimProcessor>(),
                autofacContainer.Resolve<IOAuthHelper>());

            return config;
        }
    }
}