using Lx.Utilities.Contracts.Authentication.DTOs;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Authorization;
using Lx.Utilities.Contracts.Membership.Constants;
using Lx.Utilities.Services.Infrastructure;

namespace Lx.Utilities.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        public virtual bool IsAuthorized(IAccessCriteria criteria, IdentityDto actualUser)
        {
            if (actualUser?.State == null || !actualUser.State.Equals(UserState.Active))
                return false;

            var isAuthorized = false;

            if (actualUser.Roles == null)
                return IsAuthorized(criteria, null, actualUser.UserReference);

            foreach (var role in actualUser.Roles)
            {
                if (role.RoleProcesses == null)
                {
                    isAuthorized = IsAuthorized(criteria, role.RoleType, actualUser.UserReference);
                    if (isAuthorized)
                        break;

                    continue;
                }

                foreach (var process in role.RoleProcesses)
                {
                    isAuthorized = IsAuthorized(criteria, role.RoleType, actualUser.UserReference, process.Name,
                        process.Target, process.IsDenied);
                    if (isAuthorized)
                        break;
                }

                if (isAuthorized)
                    break;
            }

            return isAuthorized;
        }

        protected virtual bool IsAuthorized(IAccessCriteria accessCriteria, string roleTypeName, string user = null,
            string process = null, string target = null, bool isDenied = false)
        {
            if (accessCriteria.IsDenied || isDenied)
                return false;

            if (roleTypeName == RoleTypeName.Admin)
                return true;

            if (!user.MatchesAnyInExpectedList(accessCriteria.Users))
                return false;

            if (!roleTypeName.MatchesAnyInExpectedList(accessCriteria.Roles))
                return false;

            return process.MatchesExpected(accessCriteria.Process) &&
                   target.MatchesExpected(accessCriteria.Target);
        }
    }
}