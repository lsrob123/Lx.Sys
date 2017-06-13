using Lx.Utilities.Contracts.Infrastructure.Common;

namespace Lx.Utilities.Contracts.Membership.Enumerations
{
    public class AccountState : Enumeration
    {
        public AccountState()
        {
        }

        public AccountState(int value, string name) : base(value, name)
        {
        }

        public static AccountState Unknown => new AccountState(0, nameof(Unknown));
        public static AccountState Lead => new AccountState(10, nameof(Lead));
        public static AccountState Account => new AccountState(20, nameof(Account));
    }
}