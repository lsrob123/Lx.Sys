using System;
using Lx.Utilities.Contracts.Authentication.Enumerations;

namespace Lx.Utilities.Contracts.Authentication.Interfaces
{
    public interface IHasVerificationFields
    {
        VerificationPurpose VerificationPurpose { get; }
        string HashedVerificationCode { get; }
        DateTimeOffset? TimeVerificationCodeSent { get; }
        DateTimeOffset? TimeVerificationCodeExpires { get; }
    }
}