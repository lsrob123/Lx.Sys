﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Lx.Utilities.Contracts.Authentication.Config;
using Lx.Utilities.Contracts.Authentication.DTOs;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Caching;

namespace Lx.Utilities.Services.Authentication
{
    public class OAuthHelper : IOAuthHelper
    {
        protected static readonly string OAuthHelperTypeName = typeof(OAuthHelper).FullName;
        protected readonly IClaimProcessor ClaimProcessor;
        protected readonly IInProcessCache InProcessCache;
        protected readonly IOAuthClientSettings Settings;

        public OAuthHelper(IOAuthClientSettings settings, IInProcessCache inProcessCache,
            IClaimProcessor claimProcessor)
        {
            Settings = settings;
            InProcessCache = inProcessCache;
            ClaimProcessor = claimProcessor ?? new StraightThroughClaimProcessor();
        }

        public virtual async Task<IdentityDto> GetUserAsync(string accessToken)
        {
            var cacheKey = OAuthHelperTypeName + nameof(IdentityDto) + accessToken;
            var user = InProcessCache.GetCachedItem<IdentityDto>(cacheKey);
            if (user != null)
                return user;

            var claims = await GetClaimsFromIdentityServiceAsync(accessToken);
            user = new IdentityDto().WithClaims(claims);

            if (user == null)
                return null;

            var expiration = Settings.AccessTokenValidationResultLifeSpan ?? TimeSpan.FromSeconds(10);
            await InProcessCache.SetCachedItemAsync(cacheKey, user, expiration);
            return user;
        }

        protected virtual async Task<ICollection<Claim>> GetClaimsFromIdentityServiceAsync(string accessToken)
        {
            var userInfoResponse = await GetUserInfoAsync(accessToken);

            if (userInfoResponse.IsError)
                return new List<Claim>();

            var claims = new List<Claim>(ClaimProcessor.Process(userInfoResponse.Claims));
            return claims;
        }

        protected virtual async Task<UserInfoResponse> GetUserInfoAsync(string accessToken)
        {
            var userInfoClient = new UserInfoClient(Settings.UserInfoEndpointAbsolutePath);

            var result = await userInfoClient.GetAsync(accessToken);

            return result;
        }
    }
}