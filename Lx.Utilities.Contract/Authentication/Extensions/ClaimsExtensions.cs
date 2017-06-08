using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Lx.Utilities.Contract.Authentication.Constants;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Membership.DTOs;
using Lx.Utilities.Contract.Membership.Enumerations;

namespace Lx.Utilities.Contract.Authentication.Extensions {
    public static class ClaimsExtensions {
        public static string GetClaimValue(this IEnumerable<Claim> claims, string claimType) {
            var value = claims?.FirstOrDefault(x => x.Type.Equals(claimType))?.Value;
            return value;
        }

        public static ICollection<RoleDto> GetRoles(this IEnumerable<Claim> claims) {
            var roles = claims?
                .Where(x => x.Type.Equals(ClaimType.Role, StringComparison.OrdinalIgnoreCase))
                .Select(x => new RoleDto {RoleType = RoleType.FromClaim(x)})
                .ToList();
            return roles;
        }
    }
}