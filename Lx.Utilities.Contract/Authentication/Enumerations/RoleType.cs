using System;
using System.Security.Claims;
using Lx.Utilities.Contract.Authentication.Constants;
using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Authentication.Enumerations {
    public class RoleType : Enumeration {
        protected RoleType(Enumeration other) : base(other) {}
        protected RoleType(int value, string name) : base(value, name) {}
        protected RoleType() {}
        public static RoleType Unknown => new RoleType(0, RoleTypeName.Unknown);
        public static RoleType BasicMember => new RoleType(10, RoleTypeName.BasicMember);
        public static RoleType Admin => new RoleType(20, RoleTypeName.Admin);

        public static RoleType FromClaim(Claim claim) {
            return !claim.Type.Equals(ClaimType.Role, StringComparison.OrdinalIgnoreCase)
                ? null
                : FromName<RoleType>(claim.Type);
        }
    }
}