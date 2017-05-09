using Lx.Utilities.Contract.Authentication.Interfaces;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class OAuthLogin : OAuthLoginClient, IOAuthLogin {
        public string GrantType { get; set; }
        public string Scopes { get; set; }
    }
}