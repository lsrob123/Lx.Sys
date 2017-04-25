using System;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.ValueObjects;

namespace Lx.Shared.All.Identity.Interfaces
{
    public interface IUserBase
    {
        UserState UserState { get; }
        PersonName Name { get; }
        bool IsAdmin { get; }
        string Username { get; }
        Email Email { get; }
        PhoneNumber MobileNumber { get; }
        Guid Key { get; }
    }
}