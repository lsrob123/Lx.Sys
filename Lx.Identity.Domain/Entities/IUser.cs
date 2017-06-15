using System;
using Lx.Identity.Contracts.Interfaces;
using Lx.Shared.All.Domains.Identity.ValueObjects;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.ValueObjects;

namespace Lx.Identity.Domain.Entities
{
    public interface IUser : IUserBase<PersonName, Email, PhoneNumber, Address>, IHasVerificationFields
    {
        string HashedPassword { get; }
        PriorUserState PriorUserState { get; }
        DateTimeOffset? TimeLockedOut { get; }
        DateTimeOffset? TimeCreated { get; }
        DateTimeOffset? TimeModified { get; }
    }
}