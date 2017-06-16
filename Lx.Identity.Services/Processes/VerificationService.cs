using System;
using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Contracts.RequestsResponses;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Identity.Services.Processes
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