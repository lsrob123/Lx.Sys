using System;
using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Contracts.RequestsResponses;
using Lx.Shared.All.Domains.Identity.Events;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Identity.Services.Processes
{
    public interface IVerificationService
    {
        ExecuteVerificationResponse ExecuteVerification(ExecuteVerificationRequest request);
        PasswordResetVerificationCodeEvent StartResetPassword(ResetPasswordRequest request);

        CreateVerificationCodeResponse CreateVerificationCode(Guid userKey, IBasicRequestKey request,
            VerificationPurpose verificationPurpose);

        T GenerateValidationFailedResponse<T>(RequestBase request, IEnumerable<string> exs)
            where T : ResponseBase, new();
    }
}