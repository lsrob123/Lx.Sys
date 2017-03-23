using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Extensions;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Membership;
using Lx.Utilities.Contract.Serialization;

namespace Lx.Utilities.Services.Authentication {
    public class OAuthHelper : IOAuthHelper {
        protected static readonly string OAuthHelperTypeName = typeof(OAuthHelper).FullName;

        protected readonly IClaimProcessor ClaimProcessor;
        protected readonly IInProcessCache InProcessCache;
        protected readonly ISerializer Serializer;
        protected readonly IOAuthClientSettings Settings;

        public OAuthHelper(IOAuthClientSettings settings, IInProcessCache inProcessCache,
            IClaimProcessor claimProcessor, ISerializer serializer) {
            Settings = settings;
            InProcessCache = inProcessCache;
            Serializer = serializer;
            ClaimProcessor = claimProcessor ?? new StraightThroughClaimProcessor();
        }

        public virtual async Task<IdentityDto> GetUserAsync(string accessToken) {
            var cacheKey = OAuthHelperTypeName + nameof(IdentityDto) + accessToken;
            var user = InProcessCache.GetCachedItem<IdentityDto>(cacheKey);
            if (user != null)
                return user;

            var claims = await GetClaimsFromIdentityServiceAsync(accessToken);
            user = new IdentityDto().WithClaims(claims,
                profileString => Serializer.Deserialize<BasicMemberInfo>(profileString));

            if (user == null)
                return null;

            var expiration = Settings.AccessTokenValidationResultLifeSpan ?? TimeSpan.FromSeconds(10);
            await InProcessCache.SetCachedItemAsync(cacheKey, user, expiration);
            return user;
        }

        protected virtual async Task<ICollection<Claim>> GetClaimsFromIdentityServiceAsync(string accessToken) {
            var userInfoResponse = await GetUserInfoAsync(accessToken);

            if (userInfoResponse.IsError)
                return new List<Claim>();

            var claims = new List<Claim>(ClaimProcessor.Process(userInfoResponse.Claims));
            return claims;
        }

        protected virtual async Task<UserInfoResponse> GetUserInfoAsync(string accessToken) {
            var userInfoClient = new UserInfoClient(Settings.UserInfoEndpointAbsolutePath);

            var result = await userInfoClient.GetAsync(accessToken);

            return result;
        }
    }
}