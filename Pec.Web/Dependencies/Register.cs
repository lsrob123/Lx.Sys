using Lx.Utilities.Contracts.Authentication.Config;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.IoC;
using Lx.Utilities.Contracts.Persistence;
using Lx.Utilities.Contracts.ServiceBus;
using Lx.Utilities.Services.Authentication;
using Pec.Web.Config;

namespace Pec.Web.Dependencies
{
    public class Register : DefaultDependencyRegisterBase
    {
        public override void AddRegistrations()
        {
            Register<IDbConfig, DbConfig>();
            Register<IBusSettings, BusSettings>();
            Register<IBusEndpointMapFactory, DefaultBusEndpointMapFactory>();
            Register<IOAuthUris, OAuthUris>();
            Register<IClaimProcessor, StraightThroughClaimProcessor>();
        }
    }
}