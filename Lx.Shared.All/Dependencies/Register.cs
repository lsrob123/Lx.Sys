using Lx.Shared.All.Identity.Config;
using Lx.Utilities.Contract.IoC;

namespace Lx.Shared.All.Dependencies {
    public class Register : DefaultDependencyRegisterBase {
        public override void AddRegistrations() {
            Register<IUserProfileConfig, UserProfileConfig>();
        }
    }
}