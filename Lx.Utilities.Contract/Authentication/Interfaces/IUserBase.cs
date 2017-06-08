using System;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Authentication.Interfaces
{
    public interface IUserBase<out TPersonName, out TEmail, out TPhoneNumber, out TAddress>
        where TPersonName : IPersonName
        where TEmail : IEmail
        where TPhoneNumber : IPhoneNumber
        where TAddress: IAddress
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