using System.Linq;
using Autofac;
using Autofac.Integration.Mvc;
using Lx.Utilities.Services.Infrastructure;

namespace Lx.Utilities.Services.IoC.AutoFac
{
    public static class ContainerBuilderExtensionsForMvc
    {
        public static ContainerBuilder RegisterWithMvc(this ContainerBuilder builder)
        {
            var assemblies = AssemblyHelper.GetReferencedAssemblies().ToArray();
            builder.RegisterControllers(assemblies);
            return builder;
        }
    }
}