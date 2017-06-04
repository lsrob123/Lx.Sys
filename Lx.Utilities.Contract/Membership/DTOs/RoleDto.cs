using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Membership.Enumerations;
using Lx.Utilities.Contract.Membership.Interfaces;

namespace Lx.Utilities.Contract.Membership.DTOs {
    public class RoleDto : IDto, IRole {
        public ICollection<RoleProcessDto> RoleProcesses { get; set; }
        public RoleType RoleType { get; set; }
    }
}