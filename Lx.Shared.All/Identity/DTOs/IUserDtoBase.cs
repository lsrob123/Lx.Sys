using System;
using Lx.Shared.All.Common.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Shared.All.Identity.DTOs {
    public interface IUserDtoBase : IDto {
        string Username { get; set; }
        EmailDto Email { get; set; }
        PhoneNumberDto MobileNumber { get; set; }
        Guid Key { get; set; }
        UserState UserState { get; set; }
        PersonNameDto Name { get; set; }
        bool IsAdmin { get; set; }
        string Nickname { get; set; }
        AvatarDto Avatar { get; set; }
    }
}