using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Authentication.DTOs
{
    public interface IUserDto : IUserBase<PersonNameDto, EmailDto, PhoneNumberDto, AddressDto>
    {
    }
}