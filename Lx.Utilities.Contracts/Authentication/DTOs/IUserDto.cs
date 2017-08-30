using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Utilities.Contracts.Authentication.DTOs
{
    public interface IUserDto : IUserBase<PersonNameDto, EmailDto, PhoneNumberDto, AddressDto>
    {
    }
}