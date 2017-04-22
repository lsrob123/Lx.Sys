using System.Linq;
using Autofac;
using Autofac.Integration.WebApi;
using Lx.Utilities.Services.Infrastructure;

namespace Lx.Utilities.Services.IoC.AutoFac
{
    public static class ContainerBuilderExtensionsForWebApi
    {
        public static ContainerBuilder RegisterWithWebApi(this ContainerBuilder builder)
        {
            var assemblies = AssemblyHelper.GetReferencedAssemblies().ToArray();
            builder.RegisterApiControllers(assemblies).InstancePerRequest();
            return builder;
        }
    }
}