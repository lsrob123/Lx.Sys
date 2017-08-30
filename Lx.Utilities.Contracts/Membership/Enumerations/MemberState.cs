using Lx.Utilities.Contracts.Infrastructure.Common;

namespace Lx.Utilities.Contracts.Membership.Enumerations
{
    public class MemberState : Enumeration
    {
        public MemberState()
        {
        }

        public MemberState(int value, string name) : base(value, name)
        {
        }

        public static MemberState Unknown => new MemberState(0, nameof(Unknown));
        public static MemberState Lead => new MemberState(10, nameof(Lead));
        public static MemberState Account => new MemberState(20, nameof(Account));
    }
}