using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Membership.Enumerations;

namespace Lx.Utilities.Contract.Membership.Interfaces
{
    public interface IMember<out TPersonName, out TEmail, out TMobileNumber, out TAddress>
        : IUserBase<TPersonName, TEmail, TMobileNumber, TAddress>
        where TPersonName : IPersonName
        where TEmail : IEmail
        where TMobileNumber : IPhoneNumber
        where TAddress : IAddress
    {
        AccountState AccountState { get; }
        string AvatarUriDefault { get; }
        string AvatarUriRelative { get; }
        string Nickname { get; }
    }
}