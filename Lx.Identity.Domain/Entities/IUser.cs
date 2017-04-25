using System;
using Lx.Identity.Contracts.Enumerations;
using Lx.Shared.All.Identity.Interfaces;
using Lx.Utilities.Contract.Authentication.Enumerations;

namespace Lx.Identity.Domain.Entities
{
    public interface IUser : IUserBase
    {
        string HashedPassword { get; }
        VerificationPurpose VerificationPurpose { get; }
        ResetPasswordMethod ResetPasswordMethod { get; }
        string HashedVerificationCode { get; }
        DateTimeOffset? TimeVerificationCodeSent { get; }
        DateTimeOffset? TimeVerificationCodeExpires { get; }
        DateTimeOffset? TimeTemporaryPasswordSent { get; }
        PriorUserState PriorUserState { get; }
        DateTimeOffset? TimeLockedOut { get; }
        DateTimeOffset? TimeCreated { get; }
        DateTimeOffset? TimeModified { get; }
    }
}