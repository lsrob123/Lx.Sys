namespace Lx.Utilities.Contract.Authentication {
    public class OAuthLogin : OAuthLoginClient, IOAuthLogin {
        public string GrantType { get; set; }
        public string Scopes { get; set; }
    }
}