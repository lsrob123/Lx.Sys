using IdentityServer3.Core.Services;
using Lx.Identity.ID3.Services;
using Lx.Identity.ID3.Stores;
using Lx.Utilities.Contract.IoC;

namespace Lx.Identity.ID3.Dependencies
{
    public class Register : DefaultDependencyRegisterBase
    {
        public override void AddRegistrations()
        {
            Register<IUserService, Id3UserService>();
            Register<IClientStore, Id3ClientStore>();
            Register<IScopeStore, Id3ScopeStore>();
            Register<ITokenHandleStore, Id3TokenHandleStore>();
            Register<IRefreshTokenStore, Id3RefreshTokenStore>();
            Register<IConsentStore, Id3ConsentStore>();
            Register<IAuthorizationCodeStore, Id3AuthorizationCodeStore>();
        }
    }
}