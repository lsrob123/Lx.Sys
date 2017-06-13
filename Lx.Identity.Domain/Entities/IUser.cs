using System;
using Lx.Identity.Contracts.Enumerations;
using Lx.Shared.All.Domains.Identity.ValueObjects;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.ValueObjects;

namespace Lx.Identity.Domain.Entities
{
    public interface IUser : IUserBase<PersonName, Email, PhoneNumber, Address>
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