using System.Collections.Generic;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public abstract class UpdateUserRequestBase : RequestBase
    {
        public UserUpdateDto UserUpdate { get; set; }
        public ICollection<UserProfileDto> UserProfiles { get; set; }
    }
}