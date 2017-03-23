using Lx.Utilities.Contract.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class GetTokensRequest : RequestBase {
        [JsonProperty(PropertyName = "oauthLogin")]
        public OAuthLogin OAuthLogin { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}