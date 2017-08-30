using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Lx.Utilities.Services.SignalR
{
    public static class HubConfigurationExtensions
    {
        public static HubConfiguration WithAutofac(this HubConfiguration hubConfig, IContainer autofacContainer,
            bool useCamelCasing = true)
        {
            hubConfig.Resolver = new AutofacDependencyResolver(autofacContainer);

            if (!useCamelCasing)
                return hubConfig;

            var jsonSettings = new JsonSerializerSettings {ContractResolver = new SignalRContractResolver()};
            var serializer = JsonSerializer.Create(jsonSettings);
            hubConfig.Resolver.Register(typeof(JsonSerializer), () => serializer);

#if DEBUG
            hubConfig.EnableDetailedErrors = true;
#endif

            return hubConfig;
        }
    }
}