using Lx.Identity.Persistence.Uow;
using Lx.Identity.Services.Services;
using Lx.Utilities.Contract.IoC;

namespace Lx.Identity.Services.Dependencies
{
    public class Register : DefaultDependencyRegisterBase
    {
        public override void AddRegistrations()
        {
            Register<IOAuthUowFactory, OAuthUowFactory>();
            Register<IUserUowFactory, UserUowFactory>();

            Register<IUserService, UserService>();
            Register<IClientService, ClientService>();
            Register<IConsentService, ConsentService>();
            Register<IScopeService, ScopeService>();
            Register<IVerificationService, VerificationService>();
        }
    }
}