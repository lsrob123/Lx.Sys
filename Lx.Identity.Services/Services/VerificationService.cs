using System;
using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Identity.Services.Services
{
    public class VerificationService : IVerificationService
    {
        public ExecuteVerificationResponse ExecuteVerification(ExecuteVerificationRequest request)
        {
            throw new NotImplementedException();
        }

        public ResetPasswordResponse ResetPassword(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public CreateVerificationCodeResponse CreateVerificationCode(Guid userKey, IBasicRequestKey request,
            VerificationPurpose verificationPurpose)
        {
            throw new NotImplementedException();
        }

        public T GenerateValidationFailedResponse<T>(RequestBase request, IEnumerable<string> exs)
            where T : ResponseBase, new()
        {
            throw new NotImplementedException();
        }
    }
}