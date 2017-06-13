using System;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Authentication.Interfaces
{
    public interface IUserBase<out TPersonName, out TEmail, out TPhoneNumber, out TAddress>
        where TPersonName : IPersonName
        where TEmail : IEmail
        where TPhoneNumber : IPhoneNumber
        where TAddress : IAddress
    {
        UserState UserState { get; }
        TPersonName PersonName { get; }
        bool IsAdmin { get; }
        string Username { get; }
        TEmail Email { get; }
        TPhoneNumber Mobile { get; }
        Guid Key { get; }
        TAddress HomeAddress { get; }
        TAddress WorkAddress { get; }
        TAddress PostalAddress { get; }
    }
}