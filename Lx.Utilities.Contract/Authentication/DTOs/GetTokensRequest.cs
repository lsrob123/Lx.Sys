using Lx.Utilities.Contract.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class GetTokensRequest : RequestBase {
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonProperty(PropertyName = "oauthLogin")]
        public OAuthLogin OAuthLogin { get; set; }
    }
}