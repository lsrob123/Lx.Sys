using System;
using Lx.Shared.All.Common.DTOs;

namespace Lx.Shared.All.Identity.DTOs {
    public interface IUserKeyEmailDto {
        Guid Key { get; set; }
        EmailDto Email { get; set; }
    }
}