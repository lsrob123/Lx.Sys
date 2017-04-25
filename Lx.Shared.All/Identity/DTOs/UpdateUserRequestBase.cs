using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Identity.DTOs
{
    public abstract class UpdateUserRequestBase : RequestBase
    {
        public UserUpdateDto UserUpdate { get; set; }
        public ICollection<UserProfileDto> UserProfiles { get; set; }
    }
}