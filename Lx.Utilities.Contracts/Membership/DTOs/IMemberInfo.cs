using System.Collections.Generic;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Membership.Interfaces;

namespace Lx.Utilities.Contracts.Membership.DTOs
{
    public interface IMemberInfo : IDto, IMember<PersonNameDto, EmailDto, PhoneNumberDto, AddressDto>
    {
        ICollection<RoleDto> Roles { get; set; }
    }
}