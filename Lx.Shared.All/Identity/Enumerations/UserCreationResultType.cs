using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Shared.All.Identity.Enumerations
{
    public class UserCreationResultType : Enumeration
    {
        public UserCreationResultType(int value, string name) : base(value, name)
        {
        }

        protected UserCreationResultType() 
        {
        }

        public static UserCreationResultType Unknown => new UserCreationResultType(0, nameof(Unknown));
        public static UserCreationResultType Created => new UserCreationResultType(10, nameof(Created));

        public static UserCreationResultType GeneralFailure => new UserCreationResultType(20,
            nameof(GeneralFailure));

        public static UserCreationResultType EmailExists => new UserCreationResultType(30, nameof(EmailExists));

        public static UserCreationResultType UsernameExists => new UserCreationResultType(40,
            nameof(UsernameExists));

        public static UserCreationResultType MobileNumberExists => new UserCreationResultType(50,
            nameof(UsernameExists));

        public static UserCreationResultType KeyExists => new UserCreationResultType(60, nameof(KeyExists));
    }
}