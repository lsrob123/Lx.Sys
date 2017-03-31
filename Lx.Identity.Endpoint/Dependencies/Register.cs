using Lx.Identity.Contracts.Config;
using Lx.Identity.Endpoint.Config;
using Lx.Utilities.Contract.IoC;

namespace Lx.Identity.Endpoint.Dependencies {
    public class Register : DefaultDependencyRegisterBase {
        public override void AddRegistrations() {
            Register<IIdentityServiceConfig, IdentityServiceConfig>();
        }
    }
}