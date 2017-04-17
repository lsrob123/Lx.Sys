using System;
using System.Collections.Generic;
using System.Security.Claims;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Membership;

namespace Lx.Utilities.Contract.Authentication.DTOs
{
    public interface IIdentityDto
    {
        UserState State { get; set; }
        ICollection<RoleDto> Roles { get; set; }
        string UserReference { get; set; }
        Guid Key { get; set; }
        bool IsAdmin { get; }
        string Email { get; set; }
        string VerifiedEmail { get; set; }
        bool IsEmailVerified { get; }
        string MobileNumber { get; set; }
        string VerifiedMobileNumber { get; set; }
        bool IsMobileNumberVerified { get; }
        string Profile { get; set; }
        ICollection<Claim> OriginalClaims { get; }
        void FromClaimsPrincipal(ClaimsPrincipal claimsPrincipal, Func<string, IMemberInfo> extractMemberInfo = null);
        void FromClaims(IEnumerable<Claim> claims, Func<string, IMemberInfo> extractMemberInfo = null);
    }
}