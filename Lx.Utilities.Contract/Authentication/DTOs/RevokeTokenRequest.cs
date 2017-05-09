using Lx.Utilities.Contract.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RevokeTokenRequest : RequestBase, IHasOAuthLoginClient {
        public string AccessTokenOrRefreshToken { get; set; }
        public string TokenHint { get; set; }

        [JsonProperty(PropertyName = "oauthClient")]
        public OAuthLoginClient OAuthClient { get; set; }
    }
}