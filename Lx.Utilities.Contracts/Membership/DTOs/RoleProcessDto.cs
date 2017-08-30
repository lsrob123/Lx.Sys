using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Membership.Interfaces;

namespace Lx.Utilities.Contracts.Membership.DTOs
{
    public class RoleProcessDto : IDto, IRoleProcess
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public bool IsDenied { get; set; }
    }
}