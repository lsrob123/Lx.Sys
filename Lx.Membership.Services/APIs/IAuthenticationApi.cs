using Lx.Shared.All.Domains.Identity.RequestsResponses;

namespace Lx.Membership.Services.APIs
{
    public interface IAuthenticationApi
    {
        void Start(ResetPasswordRequest request);
    }
}