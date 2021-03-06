﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Lx.Utilities.Contracts.Authentication.Constants;
using Lx.Utilities.Contracts.Membership.DTOs;

namespace Lx.Utilities.Contracts.Authentication.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetClaimValue(this IEnumerable<Claim> claims, string claimType)
        {
            var value = claims?.FirstOrDefault(x => x.Type.Equals(claimType))?.Value;
            return value;
        }

        public static ICollection<RoleDto> GetRoles(this IEnumerable<Claim> claims)
        {
            var roles = claims?
                .Where(x => x.Type.Equals(ClaimType.Role, StringComparison.OrdinalIgnoreCase))
                .Select(x => new RoleDto
                {
                    RoleType = x.Type.Equals(ClaimType.Role, StringComparison.OrdinalIgnoreCase)
                        ? x.Type
                        : null
                })
                .ToList();
            return roles;
        }
    }
}