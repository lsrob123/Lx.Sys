using System;
using System.Collections.Generic;
using System.Security.Claims;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Membership;

namespace Lx.Utilities.Contract.Authentication.Extensions {
    public static class IdentityDtoExtensions {
        public static TIdentityDto WithClaimsPrincipal<TIdentityDto>(this TIdentityDto identityDto,
            ClaimsPrincipal claimsPrincipal, Func<string, IMemberInfo> extractMemberInfo)
            where TIdentityDto : IIdentityDto {
            identityDto.FromClaimsPrincipal(claimsPrincipal, extractMemberInfo);
            return identityDto;
        }

        public static TIdentityDto WithClaims<TIdentityDto>(this TIdentityDto identityDto,
            IEnumerable<Claim> claims, Func<string, IMemberInfo> extractMemberInfo)
            where TIdentityDto : IIdentityDto {
            identityDto.FromClaims(claims, extractMemberInfo);
            return identityDto;
        }
    }
}