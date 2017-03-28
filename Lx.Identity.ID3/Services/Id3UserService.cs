using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using Lx.Identity.Contracts.Config;
using Lx.Identity.Services.Services;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Crypto;

namespace Lx.Identity.ID3.Services
{
    public class Id3UserService : UserServiceBase
    {
        protected readonly IUserService BackingUserService;
        protected readonly IClientService ClientService;
        protected readonly ICryptoService CryptoService;
        protected readonly IIdentityConfig IdentityConfig;

        public Id3UserService(IUserService backingUserService, IClientService clientService,
            ICryptoService cryptoService, IIdentityConfig identityConfig)
        {
            BackingUserService = backingUserService;
            ClientService = clientService;
            CryptoService = cryptoService;
            IdentityConfig = identityConfig;
        }

        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = await Task.Run(() => BackingUserService.GetUser(context.UserName));
            if ((user == null) || !user.UserState.Equals(UserState.Active))
            {
                context.AuthenticateResult = new AuthenticateResult("Invalid user");
                return;
            }

            var passwordValid = CryptoService.Validate(context.Password, user.HashedPassword);
            if (!passwordValid)
            {
                context.AuthenticateResult = new AuthenticateResult("Invalid user or password");
                return;
            }

            var claims = await GetClaimsAsync(context.SignInMessage.ClientId, user);

            context.AuthenticateResult = new AuthenticateResult(user.Key.ToString(), user.Username,
                identityProvider: IdentityProviderName, claims: claims);
        }
    }
}