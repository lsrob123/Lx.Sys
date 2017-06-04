using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Membership.Interfaces;

namespace Lx.Utilities.Contract.Membership.DTOs
{
    public interface IMemberInfo : IDto, IMember<PersonNameDto, EmailDto, PhoneNumberDto, AddressDto>
    {
        ICollection<RoleDto> Roles { get; set; }
    }
}