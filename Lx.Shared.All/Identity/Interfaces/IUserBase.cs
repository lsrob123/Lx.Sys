using System;
using Lx.Shared.All.Identity.ValueObjects;
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
        string Nickname { get; }
        Avatar Avatar { get; }
    }
}