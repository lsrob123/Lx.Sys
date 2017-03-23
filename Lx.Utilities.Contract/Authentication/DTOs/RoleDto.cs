using System.Collections.Generic;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RoleDto {
        public virtual string RoleType { get; set; }
        public virtual List<RoleProcessDto> Processes { get; set; }
    }
}