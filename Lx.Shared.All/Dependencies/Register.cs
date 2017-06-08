using Lx.Shared.All.Domains.Identity.Config;
using Lx.Utilities.Contract.IoC;

namespace Lx.Shared.All.Dependencies {
    public class Register : DefaultDependencyRegisterBase {
        public override void AddRegistrations() {
            Register<IUserProfileConfig, UserProfileConfig>();
            Register<ICommonBusEndpointSettings, CommonBusEndpointSettings>();
        }
    }
}