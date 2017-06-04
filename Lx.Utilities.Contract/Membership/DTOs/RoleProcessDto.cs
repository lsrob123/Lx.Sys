using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Membership.Interfaces;

namespace Lx.Utilities.Contract.Membership.DTOs {
    public class RoleProcessDto : IDto, IRoleProcess {
        public string Name { get; set; }
        public string Target { get; set; }
        public bool IsDenied { get; set; }
    }
}