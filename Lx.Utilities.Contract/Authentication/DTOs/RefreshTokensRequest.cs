using Lx.Utilities.Contract.Infrastructure.Dto;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RefreshTokensRequest : RequestBase {
        [JsonProperty(PropertyName = "oauthClient")]
        public OAuthLoginClient OAuthClient { get; set; }

        public string RefreshToken { get; set; }
    }
}