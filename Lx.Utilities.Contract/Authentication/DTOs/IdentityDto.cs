using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using Lx.Utilities.Contract.Authentication.Constants;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Authentication.Extensions;
using Lx.Utilities.Contract.Membership;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Authentication.DTOs
{
    public class IdentityDto : IIdentityDto
    {
        public ICollection<RoleDto> Roles { get; set; }
        public UserState State { get; set; }
        public string AvatarUriDefault { get; set; }
        public string AvatarUriRelative { get; set; }

        public void FromClaims(IEnumerable<Claim> claims, Func<string, IMemberInfo> extractMemberInfo)
        {
            OriginalClaims = claims?.ToList();
            if ((OriginalClaims == null) || !OriginalClaims.Any())
                return;

            UserReference = OriginalClaims.GetClaimValue(ClaimType.Subject)?.Trim();

            Guid userKey;
            if (!string.IsNullOrWhiteSpace(UserReference) && Guid.TryParse(UserReference, out userKey))
                Key = userKey;
            else
                Key = Guid.Empty;

            Email = OriginalClaims.GetClaimValue(ClaimType.Email);
            VerifiedEmail = OriginalClaims.GetClaimValue(ClaimType.VerifiedEmail);
            MobileNumber = OriginalClaims.GetClaimValue(ClaimType.PhoneNumber);
            VerifiedMobileNumber = OriginalClaims.GetClaimValue(ClaimType.VerifiedPhoneNumber);
            AvatarUriDefault = OriginalClaims.GetClaimValue(ClaimType.AvatarUriDefault);
            AvatarUriRelative = OriginalClaims.GetClaimValue(ClaimType.AvatarUriRelative);

            Profile = OriginalClaims.GetClaimValue(ClaimType.Profile);

            var memberInfo = extractMemberInfo?.Invoke(Profile);

            var rolesInClaims = OriginalClaims.GetRoles() ?? new List<RoleDto>();
            if (memberInfo == null)
            {
                State = UserState.MemberInfoNotFound;
                Roles = rolesInClaims;
            }
            else
            {
                State = memberInfo.State;
                var roles = memberInfo.Roles?.ToDictionary(x => x.RoleType) ?? new Dictionary<string, RoleDto>();
                // ReSharper disable once LoopCanBePartlyConvertedToQuery
                foreach (var role in rolesInClaims)
                    if (!roles.ContainsKey(role.RoleType))
                        roles.Add(role.RoleType, role);
                Roles = roles.Values.ToList();
            }
        }

        [IgnoreDataMember]
        [JsonIgnore]
        public ICollection<Claim> OriginalClaims { get; protected set; }

        public string Profile { get; set; }

        public string UserReference { get; set; }

        public Guid Key { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public bool IsAdmin =>
            (Roles != null) && Roles.Any(x =>
                !string.IsNullOrWhiteSpace(x.RoleType) &&
                x.RoleType.Equals(RoleTypeName.Admin, StringComparison.OrdinalIgnoreCase));

        public string Email { get; set; }
        public string VerifiedEmail { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public bool IsEmailVerified =>
            !string.IsNullOrWhiteSpace(VerifiedEmail) && !string.IsNullOrWhiteSpace(Email) &&
            VerifiedEmail.Equals(Email, StringComparison.OrdinalIgnoreCase);

        public string MobileNumber { get; set; }
        public string VerifiedMobileNumber { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public bool IsMobileNumberVerified =>
            !string.IsNullOrWhiteSpace(VerifiedMobileNumber) && !string.IsNullOrWhiteSpace(MobileNumber) &&
            VerifiedMobileNumber.Equals(MobileNumber, StringComparison.OrdinalIgnoreCase);
    }
}