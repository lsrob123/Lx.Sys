using System;
using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Contracts.RequestsResponses;
using Lx.Identity.Persistence.Uow;
using Lx.Shared.All.Domains.Identity.Events;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Crypto;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Extensions;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Services.Infrastructure;

namespace Lx.Identity.Services.Processes
{
    public class VerificationService : IVerificationService
    {
        private readonly IUserUowFactory _userUowFactory;
        private readonly ICryptoService _cryptoService;
        
        public VerificationService(IUserUowFactory userUowFactory, ICryptoService cryptoService)
        {
            _userUowFactory = userUowFactory;
            _cryptoService = cryptoService;
        }

        public ExecuteVerificationResponse ExecuteVerification(ExecuteVerificationRequest request)
        {
            throw new NotImplementedException();
        }

        public PasswordResetVerificationCodeEvent StartResetPassword(ResetPasswordRequest request)
        {
            var verificationCode = new ShortGuid(Guid.NewGuid());
            var hashedVerificationCode = _cryptoService.CreateHash(verificationCode);
            var userDto = _userUowFactory.SetVerificationCode(request.Email, VerificationPurpose.ResetPassword,
                hashedVerificationCode, DateTimeOffset.UtcNow.AddMinutes(5));
            var verificationCodeEvent = new PasswordResetVerificationCodeEvent
            {

            }.LinkTo(request);
            return verificationCodeEvent;
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