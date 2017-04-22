using Lx.Identity.Contracts.Config;
using Lx.Identity.Endpoint.Config;
using Lx.Utilities.Contract.IoC;
using Lx.Utilities.Contract.Web;

namespace Lx.Identity.Endpoint.Dependencies {
    public class Register : DefaultDependencyRegisterBase {
        public override void AddRegistrations() {
            Register<IIdentityServiceConfig, IdentityServiceConfig>();
            Register<IVerificationCodeConfig, VerificationCodeConfig>();
            Register<IWebEndpointSettings, WebEndpointSettings>();
        }
    }
}