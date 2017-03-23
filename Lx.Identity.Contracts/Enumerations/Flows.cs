using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Identity.Contracts.Enumerations {
    public class Flows : Enumeration {
        protected Flows(int value, string name) : base(value, name) {}

        protected Flows() {}
        public static Flows AuthorizationCode => new Flows(0, nameof(AuthorizationCode));
        public static Flows Implicit => new Flows(1, nameof(Implicit));
        public static Flows Hybrid => new Flows(2, nameof(Hybrid));
        public static Flows ClientCredentials => new Flows(3, nameof(ClientCredentials));
        public static Flows ResourceOwner => new Flows(4, nameof(ResourceOwner));
        public static Flows Custom => new Flows(5, nameof(Custom));
        public static Flows AuthorizationCodeWithProofKey => new Flows(6, nameof(AuthorizationCodeWithProofKey));
        public static Flows HybridWithProofKey => new Flows(7, nameof(HybridWithProofKey));
    }
}