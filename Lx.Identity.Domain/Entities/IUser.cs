using System;
using Lx.Identity.Contracts.Enumerations;
using Lx.Shared.All.Domains.Identity.Interfaces;
using Lx.Shared.All.Domains.Identity.ValueObjects;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.ValueObjects;

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