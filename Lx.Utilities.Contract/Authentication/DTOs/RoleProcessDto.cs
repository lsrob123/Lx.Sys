using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RoleProcessDto : IDto, IRoleProcess {
        public string Name { get; set; }
        public string Target { get; set; }
        public bool IsDenied { get; set; }
    }
}