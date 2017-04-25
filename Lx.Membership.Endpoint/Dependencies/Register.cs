using Lx.Membership.Endpoint.Config;
using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Contract.IoC;
using Lx.Utilities.Contract.Persistence;
using Lx.Utilities.Contract.ServiceBus;
using Lx.Utilities.Contract.Web;
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