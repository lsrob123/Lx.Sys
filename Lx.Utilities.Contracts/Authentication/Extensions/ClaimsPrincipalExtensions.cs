using System.Collections.Generic;
using System.Security.Claims;
using Lx.Utilities.Contracts.Membership.DTOs;

namespace Lx.Utilities.Contracts.Authentication.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal?.Claims?.GetClaimValue(claimType);
        }

        public static ICollection<RoleDto> GetRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims?.GetRoles();
        }
    }
}