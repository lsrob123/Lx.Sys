using Lx.Utilities.Contracts.Infrastructure.Common;

namespace Lx.Utilities.Contracts.Infrastructure.Enumerations
{
    public class Privilege : Enumeration
    {
        public static readonly Privilege Granted = new Privilege(0, nameof(Granted));
        public static readonly Privilege Denied = new Privilege(10, nameof(Denied));

        public Privilege(int value, string name) : base(value, name)
        {
        }

        protected Privilege()
        {
        }
    }
}