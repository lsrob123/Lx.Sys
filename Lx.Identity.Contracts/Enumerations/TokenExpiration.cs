using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Identity.Contracts.Enumerations {
    public class TokenExpiration : Enumeration {
        protected TokenExpiration(int value, string name) : base(value, name) {}
        protected TokenExpiration() {}

        public static TokenExpiration Sliding => new TokenExpiration(0, nameof(Sliding));
        public static TokenExpiration Absolute => new TokenExpiration(1, nameof(Absolute));
    }
}