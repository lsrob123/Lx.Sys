using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Services.IoC.AutoFac {
    public static class ContainerBuilderExtensionsForWebApi {
        public static ContainerBuilder RegisterWithWebApi(this ContainerBuilder builder) {
            var config = new HttpConfiguration();
            builder.RegisterInstance(config);
            builder.RegisterApiControllers(AssemblyHelper.GetReferencedAssemblies().ToArray()).InstancePerRequest();
            builder.RegisterWebApiFilterProvider(config);
            return builder;
        }
    }
}