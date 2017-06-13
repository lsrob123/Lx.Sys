using System;
using Lx.Utilities.Contracts.Authentication.Enumerations;

namespace Lx.Identity.Contracts.Config
{
    public interface IVerificationCodeConfig
    {
        TimeSpan EmailVerificationCodeLiveSpan { get; }
        TimeSpan MobileVerificationCodeLiveSpan { get; }
        TimeSpan PasswordlessLoginVerificationCodeLiveSpan { get; }
        TimeSpan GetLiveSpan(VerificationPurpose purpose);
    }
}