using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Identity.Contracts.Enumerations {
    public class ResetPasswordMethod : Enumeration {
        protected ResetPasswordMethod(int value, string name) : base(value, name) {}

        protected ResetPasswordMethod() {}

        public static ResetPasswordMethod Nothing => new ResetPasswordMethod(10,
            nameof(Nothing));

        public static ResetPasswordMethod ResetPasswordByEmail => new ResetPasswordMethod(20,
            nameof(ResetPasswordByEmail));

        public static ResetPasswordMethod ResetPasswordByMobile => new ResetPasswordMethod(30,
            nameof(ResetPasswordByMobile));
    }
}