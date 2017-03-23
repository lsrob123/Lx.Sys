using Lx.Utilities.Contract.Infrastructure.DTO;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RevokeTokenRequest : RequestBase {
        [JsonProperty(PropertyName = "oauthClient")]
        public OAuthLoginClient OAuthClient { get; set; }

        public string AccessTokenOrRefreshToken { get; set; }

        public string TokenHint { get; set; }
    }
}