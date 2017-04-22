using Lx.Utilities.Contract.Authentication.Interfaces;

namespace Lx.Utilities.Contract.Authentication.DTOs
{
    public class RoleProcessDto : IRoleProcess
    {
        public virtual string Name { get; set; }
        public virtual string Target { get; set; }
        public virtual bool IsDenied { get; set; }
    }
}