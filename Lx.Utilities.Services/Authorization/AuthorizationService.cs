﻿using Lx.Utilities.Contract.Authentication.Constants;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authorization;
using Lx.Utilities.Contract.Enumerations.Identity;
using Lx.Utilities.Services.Infrastructure;

namespace Lx.Utilities.Services.Authorization {
    public class AuthorizationService : IAuthorizationService {
        public virtual bool IsAuthorized(IAccessCriteria criteria, IdentityDto actualUser) {
            if (!actualUser.State.Equals(UserState.Active))
                return false;

            var isAuthorized = false;

            if (actualUser.Roles == null)
                return IsAuthorized(criteria, null, actualUser.UserReference);

            foreach (var role in actualUser.Roles) {
                if (role.Processes == null) {
                    isAuthorized = IsAuthorized(criteria, role.RoleType, actualUser.UserReference);
                    if (isAuthorized)
                        break;

                    continue;
                }

                foreach (var process in role.Processes) {
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

        protected virtual bool IsAuthorized(IAccessCriteria accessCriteria, string role, string user = null,
            string process = null, string target = null, bool isDenied = false) {
            if (accessCriteria.IsDenied || isDenied)
                return false;

            if (role == RoleTypeName.Admin)
                return true;

            if (!user.MatchesAnyInExpectedList(accessCriteria.Users))
                return false;

            if (!role.MatchesAnyInExpectedList(accessCriteria.Roles))
                return false;

            return process.MatchesExpected(accessCriteria.Process) &&
                   target.MatchesExpected(accessCriteria.Target);
        }
    }
}