using System;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs {
    public interface IUserKeyEmailDto {
        Guid Key { get; set; }
        EmailDto Email { get; set; }
    }
}