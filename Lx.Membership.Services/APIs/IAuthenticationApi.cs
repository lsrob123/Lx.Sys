using System;
using Lx.Shared.All.Domains.Identity.RequestsResponses;

namespace Lx.Membership.Services.APIs
{
    public interface IAuthenticationApi
    {
        void Start(CreatePasswordResetVerificationCodeRequest request);
        void Start(ResetPasswordRequest request);
    }
}