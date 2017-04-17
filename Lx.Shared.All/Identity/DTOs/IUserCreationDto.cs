using System.Collections.Generic;

namespace Lx.Shared.All.Identity.DTOs
{
    public interface IUserCreationDto : IUserDtoBase
    {
        string PlainTextPassword { get; set; }
        ICollection<UserProfileDto> UserProfiles { get; set; }
    }
}