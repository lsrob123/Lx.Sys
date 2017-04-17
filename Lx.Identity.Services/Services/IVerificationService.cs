using System;
using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Identity.Services.Services
{
    public interface IVerificationService
    {
        ExecuteVerificationResponse ExecuteVerification(ExecuteVerificationRequest request);
        ResetPasswordResponse ResetPassword(ResetPasswordRequest request);

        CreateVerificationCodeResponse CreateVerificationCode(Guid userKey, IBasicRequestKey request,
            VerificationPurpose verificationPurpose);

        T GenerateValidationFailedResponse<T>(RequestBase request, IEnumerable<string> exs)
            where T : ResponseBase, new();
    }
}