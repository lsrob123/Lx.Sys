using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Identity.Contracts.Enumerations {
    public class TokenUsage : Enumeration {
        protected TokenUsage(int value, string name) : base(value, name) {}

        protected TokenUsage() {}
        public static TokenUsage ReUse => new TokenUsage(0, nameof(ReUse));
        public static TokenUsage OneTimeOnly => new TokenUsage(1, nameof(OneTimeOnly));
    }
}