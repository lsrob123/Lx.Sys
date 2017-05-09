using Lx.Utilities.Contract.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RefreshTokensRequest : RequestBase, IHasOAuthLoginClient {
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "oauthClient")]
        public OAuthLoginClient OAuthClient { get; set; }
    }
}