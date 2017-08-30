using Lx.Utilities.Contracts.Infrastructure.Common;

namespace Lx.Identity.Contracts.Enumerations
{
    public class VerificationResult : Enumeration
    {
        protected VerificationResult(int value, string name, string message) : base(value, name)
        {
            Message = message;
        }

        protected VerificationResult()
        {
        }

        public string Message { get; set; }

        public static VerificationResult Unknown => new VerificationResult(0,
            nameof(Unknown), "Unknow result was returned.");

        public static VerificationResult Passed => new VerificationResult(10,
            nameof(Passed), string.Empty);

        public static VerificationResult InvalidCode => new VerificationResult(20,
            nameof(InvalidCode), "Verification code is invalid.");

        public static VerificationResult ExpiredCode => new VerificationResult(30,
            nameof(ExpiredCode), "Verification code has expired.");

        public static VerificationResult NewVerificationCodeRequired => new VerificationResult(40,
            nameof(NewVerificationCodeRequired), "Please request a new verification code.");
    }
}