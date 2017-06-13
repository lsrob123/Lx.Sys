using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using Lx.Identity.Services.Services;
using Lx.Utilities.Contracts.Authentication.Config;
using Lx.Utilities.Contracts.Authentication.DTOs;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Crypto;
using Lx.Utilities.Contracts.Membership.Constants;

namespace Lx.Identity.ID3.Services
{
    public class Id3UserService : UserServiceBase
    {
        protected readonly IUserService BackingUserService;
        protected readonly IClientService ClientService;
        protected readonly ICryptoService CryptoService;
        protected readonly IIdentityServiceConfig IdentityServiceConfig;

        public Id3UserService(IUserService userService, IClientService clientService,
            ICryptoService cryptoService, IIdentityServiceConfig identityServiceConfig)
        {
            BackingUserService = userService;
            ClientService = clientService;
            CryptoService = cryptoService;
            IdentityServiceConfig = identityServiceConfig;
        }

        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = await Task.Run(() => BackingUserService.GetUser(context.UserName, null));
            if (user == null || !user.UserState.Equals(UserState.Active))
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
                identityProvider: IdentityServiceConfig.IdentityProviderName, claims: claims);
        }

        public override async Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            context.AuthenticateResult = new AuthenticateResult("Authentication failed");
            await Task.CompletedTask;
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await GetUserBySubjectAsync(context.Subject);
            if (user == null)
                return;

            context.IssuedClaims = await GetClaimsAsync(context.Client.ClientId, user);
        }

        private async Task<IReadOnlyCollection<Claim>> GetClaimsAsync(string clientId, IUserDto user)
        {
            var claims = new List<Claim>();
            var client = await Task.Run(() => ClientService.GetClientByClientId(clientId));
            var userProfile =
                await Task.Run(() => BackingUserService.GetUserProfile(user.Key, client.UserProfileOriginator));
            //make sure user is within expected profile group

            if (userProfile == null)
                return claims;

            claims.Add(new Claim(Constants.ClaimTypes.Subject, user.Key.ToString()));

            if (!string.IsNullOrWhiteSpace(user.Email.Address))
            {
                claims.Add(new Claim(Constants.ClaimTypes.Email, user.Email.Address));
                if (user.Email.Verified)
                    claims.Add(new Claim(Constants.ClaimTypes.EmailVerified, user.Email.Address));
            }

            if (!string.IsNullOrWhiteSpace(user.Mobile.FullNumber))
            {
                claims.Add(new Claim(Constants.ClaimTypes.PhoneNumber, user.Mobile.FullNumber));
                if (user.Mobile.Verified)
                    claims.Add(new Claim(Constants.ClaimTypes.PhoneNumberVerified, user.Mobile.FullNumber));
            }

            if (!string.IsNullOrWhiteSpace(userProfile.Body))
                claims.Add(new Claim(Constants.ClaimTypes.Profile, userProfile.Body));

            if (user.IsAdmin)
                claims.Add(new Claim(Constants.ClaimTypes.Role, RoleTypeName.Admin));

            return claims;
        }

        public override async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await GetUserBySubjectAsync(context.Subject);
            if (user == null)
            {
                context.IsActive = false;
                return;
            }

            var client =
                await Task.Run(() => ClientService.GetClientByClientId(context.Client.ClientId));
            var userProfile =
                await Task.Run(() => BackingUserService.GetUserProfile(user.Key, client.UserProfileOriginator));
            //make sure user is within expected profile group

            context.IsActive = user.UserState.Equals(UserState.Active) && userProfile != null;
        }

        protected async Task<IUserDto> GetUserBySubjectAsync(IPrincipal subject)
        {
            var subjectId = subject.GetSubjectId();
            Guid userKey;

            if (subjectId == null || !Guid.TryParse(subjectId, out userKey))
                return null;

            var user = await Task.Run(() => BackingUserService.GetUser(userKey, null));
            return user;
        }
    }
}