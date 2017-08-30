using Lx.Shared.All.Domains.Communications;
using Lx.Shared.All.Domains.Identity.Config;
using Lx.Utilities.Contracts.IoC;

namespace Lx.Shared.All.Dependencies
{
    public class Register : DefaultDependencyRegisterBase
    {
        public override void AddRegistrations()
        {
            Register<IUserProfileConfig, UserProfileConfig>();
            Register<ICommonBusEndpointSettings, CommonBusEndpointSettings>();
            Register<IEmailSenderConfig, EmailSenderConfig>();
        }
    }
}