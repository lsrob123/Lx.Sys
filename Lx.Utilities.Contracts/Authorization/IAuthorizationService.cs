using Lx.Utilities.Contracts.Authentication.DTOs;

namespace Lx.Utilities.Contracts.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized(IAccessCriteria criteria, IdentityDto actualUser);
    }
}