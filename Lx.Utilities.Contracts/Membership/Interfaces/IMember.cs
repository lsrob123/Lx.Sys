using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Membership.Enumerations;

namespace Lx.Utilities.Contracts.Membership.Interfaces
{
    public interface IMember<out TPersonName, out TEmail, out TMobileNumber, out TAddress>
        : IUserBase<TPersonName, TEmail, TMobileNumber, TAddress>
        where TPersonName : IPersonName
        where TEmail : IEmail
        where TMobileNumber : IPhoneNumber
        where TAddress : IAddress
    {
        MemberState MemberState { get; }
        string AvatarUriDefault { get; }
        string AvatarUriRelative { get; }
        string Nickname { get; }
    }
}