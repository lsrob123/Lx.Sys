using Lx.Utilities.Contract.Authentication.DTOs;

namespace Lx.Utilities.Contract.Authorization {
    public interface IAuthorizationService {
        bool IsAuthorized(IAccessCriteria criteria, IdentityDto actualUser);
    }
}