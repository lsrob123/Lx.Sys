using System;
using System.Collections.Generic;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Membership.Interfaces;

namespace Lx.Utilities.Contracts.Membership.DTOs
{
    public class RoleDto : IDto, IRole
    {
        public ICollection<RoleProcessDto> RoleProcesses { get; set; }
        public Guid UserKey { get; set; }
        public string RoleType { get; set; }
    }
}