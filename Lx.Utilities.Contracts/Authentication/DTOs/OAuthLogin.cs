using Lx.Utilities.Contracts.Authentication.Interfaces;

namespace Lx.Utilities.Contracts.Authentication.DTOs
{
    public class OAuthLogin : OAuthLoginClient, IOAuthLogin
    {
        public string GrantType { get; set; }
        public string Scopes { get; set; }
    }
}