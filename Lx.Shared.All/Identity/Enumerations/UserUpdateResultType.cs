using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Shared.All.Identity.Enumerations
{
    public class UserUpdateResultType : Enumeration
    {
        public UserUpdateResultType(int value, string name) : base(value, name)
        {
        }

        protected UserUpdateResultType() 
        {
        }

        public static UserUpdateResultType Unknown => new UserUpdateResultType(0, nameof(Unknown));
        public static UserUpdateResultType Created => new UserUpdateResultType(10, nameof(Created));
        public static UserUpdateResultType Updated => new UserUpdateResultType(20, nameof(Updated));

        public static UserUpdateResultType GeneralFailure => new UserUpdateResultType(30,
            nameof(GeneralFailure));

        public static UserUpdateResultType EmailExists => new UserUpdateResultType(40, nameof(EmailExists));

        public static UserUpdateResultType UsernameExists => new UserUpdateResultType(50,
            nameof(UsernameExists));

        public static UserUpdateResultType MobileNumberExists => new UserUpdateResultType(60,
            nameof(UsernameExists));

        public static UserUpdateResultType KeyExists => new UserUpdateResultType(70, nameof(KeyExists));
    }
}