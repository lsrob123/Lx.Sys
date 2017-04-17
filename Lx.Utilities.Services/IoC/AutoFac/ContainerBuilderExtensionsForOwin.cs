using Autofac;
using Microsoft.Owin;

namespace Lx.Utilities.Services.IoC.AutoFac
{
    public static class ContainerBuilderExtensionsForOwin
    {
        public static ContainerBuilder RegisterPerRequestOwinMiddleware<T>(this ContainerBuilder builder)
            where T : OwinMiddleware
        {
            builder.RegisterType<T>().InstancePerRequest();
            return builder;
        }
    }
}