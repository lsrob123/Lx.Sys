﻿using System.Collections.Generic;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RoleDto : IDto, IRole {
        public ICollection<RoleProcessDto> RoleProcesses { get; set; }
        public RoleType RoleType { get; set; }
    }
}