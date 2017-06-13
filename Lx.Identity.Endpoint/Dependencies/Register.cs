using Lx.Identity.Contracts.Config;
using Lx.Identity.Endpoint.Config;
using Lx.Utilities.Contracts.Authentication.Config;
using Lx.Utilities.Contracts.IoC;
using Lx.Utilities.Contracts.Persistence;
using Lx.Utilities.Contracts.ServiceBus;
using Lx.Utilities.Contracts.Web;

namespace Lx.Identity.Endpoint.Dependencies
{
    public class Register : DefaultDependencyRegisterBase
    {
        public override void AddRegistrations()
        {
            Register<IIdentityServiceConfig, IdentityServiceConfig>();
            Register<IVerificationCodeConfig, VerificationCodeConfig>();
            Register<IWebEndpointSettings, WebEndpointSettings>();
            Register<IDbConfig, DbConfig>();
            Register<IBusSettings, BusSettings>();
        }
    }
}