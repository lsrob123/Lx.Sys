using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Filters;
using Autofac;
using Autofac.Integration.WebApi;

namespace Lx.Utilities.Services.Web
{
    public static class HttpConfigurationExtensions
    {
        public static HttpConfiguration WithAutofac(this HttpConfiguration config, IContainer autofacContainer)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(autofacContainer);
            return config;
        }

        public static HttpConfiguration WithDefaultSettings(this HttpConfiguration config,
            IEnumerable<IFilter> filters = null, bool mapAttributeRoutes = true)
        {
            if (mapAttributeRoutes)
                config.MapHttpAttributeRoutes();

            if (filters == null)
                filters = new List<IFilter>();
            config.Filters.AddRange(filters);

            return config;
        }
    }
}