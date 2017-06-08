using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Membership;
using Lx.Utilities.Contract.Membership.DTOs;
using Lx.Utilities.Services.Serialization;

namespace Lx.Utilities.Services.Authentication
{
    public static class IdentityDtoExtensions
    {
        public static bool IsInRole<TUser>(this TUser user, string roleTypeName) where TUser : IdentityDto
        {
            return Includes(user.Roles, roleTypeName);
        }

        public static bool Includes(this IEnumerable<RoleDto> roles, string roleTypeName)
        {
            var roleList = roles?.ToList();
            var isInRole = roleList != null &&
                           roleList.Any(x => x.RoleType.Equals(roleTypeName, StringComparison.OrdinalIgnoreCase));
            return isInRole;
        }

        public static TIdentityDto WithClaimsPrincipal<TIdentityDto>(this TIdentityDto identityDto,
            ClaimsPrincipal claimsPrincipal, Func<string, IMemberInfo> extractMemberInfo)
            where TIdentityDto : IIdentityDto
        {
            return identityDto.WithClaims(claimsPrincipal?.Claims, extractMemberInfo);
        }

        public static TIdentityDto WithClaims<TIdentityDto>(this TIdentityDto identityDto,
            IEnumerable<Claim> claims, Func<string, IMemberInfo> extractMemberInfo = null)
            where TIdentityDto : IIdentityDto
        {
            if (extractMemberInfo == null)
            {
                var serializer = new JsonSerializer();
                extractMemberInfo = x => serializer.Deserialize<BasicMemberInfo>(x);
            }

            identityDto.FromClaims(claims, extractMemberInfo);
            return identityDto;
        }

        public static TIdentityDto WithPrincipal<TIdentityDto>(this TIdentityDto identityDto,
            IPrincipal principal, Func<string, IMemberInfo> extractMemberInfo = null)
            where TIdentityDto : IIdentityDto
        {
            return identityDto.WithClaimsPrincipal(principal as ClaimsPrincipal, extractMemberInfo);
        }
    }
}