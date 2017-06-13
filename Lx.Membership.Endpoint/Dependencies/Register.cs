using Lx.Membership.Endpoint.Config;
using Lx.Utilities.Contracts.Authentication.Config;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.IoC;
using Lx.Utilities.Contracts.Persistence;
using Lx.Utilities.Contracts.ServiceBus;
using Lx.Utilities.Contracts.Web;
using Lx.Utilities.Services.Authentication;

namespace Lx.Membership.Endpoint.Dependencies
{
    public class Register : DefaultDependencyRegisterBase
    {
        public override void AddRegistrations()
        {
            //Register<IVerificationCodeConfig, VerificationCodeConfig>();
            Register<IWebEndpointSettings, WebEndpointSettings>();
            Register<IDbConfig, DbConfig>();
            Register<IBusSettings, BusSettings>();
            Register<IBusEndpointMapFactory, DefaultBusEndpointMapFactory>();
            Register<IOAuthUris, OAuthUris>();
            Register<IClaimProcessor, StraightThroughClaimProcessor>();
        }
    }
}