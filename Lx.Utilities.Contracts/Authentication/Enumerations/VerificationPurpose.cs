using Lx.Utilities.Contracts.Infrastructure.Common;

namespace Lx.Utilities.Contracts.Authentication.Enumerations
{
    public class VerificationPurpose : Enumeration
    {
        protected VerificationPurpose(int value, string name) : base(value, name)
        {
        }

        protected VerificationPurpose()
        {
        }

        public static VerificationPurpose Nothing => new VerificationPurpose(10,
            nameof(Nothing));

        public static VerificationPurpose VerifyEmail => new VerificationPurpose(20,
            nameof(VerifyEmail));

        public static VerificationPurpose VerifyMobile => new VerificationPurpose(30,
            nameof(VerifyMobile));

        public static VerificationPurpose PasswordlessLogin => new VerificationPurpose(40,
            nameof(PasswordlessLogin));
    }
}