using System;
using System.Linq;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public static class IdentityDtoExtensions {
        public static bool IsInRole<TUser>(this TUser user, string roleTypeName) where TUser : IdentityDto {
            var isInRole = (user.Roles != null) &&
                           user.Roles.Any(x => x.RoleType.Equals(roleTypeName, StringComparison.OrdinalIgnoreCase));
            return isInRole;
        }
    }
}