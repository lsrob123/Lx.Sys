using Lx.Utilities.Contracts.Infrastructure.Common;

namespace Lx.Identity.Contracts.Enumerations
{
    public class AccessTokenType : Enumeration
    {
        protected AccessTokenType(int value, string name) : base(value, name)
        {
        }

        protected AccessTokenType()
        {
        }

        public static AccessTokenType Jwt => new AccessTokenType(0, nameof(Jwt));
        public static AccessTokenType Reference => new AccessTokenType(1, nameof(Reference));
    }
}