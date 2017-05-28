using Lx.Utilities.Contract.Infrastructure.Attributes;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Utilities.Contract.Authentication.DTOs {
    public class RefreshTokensRequest : RequestBase, IHasOAuthLoginClient {
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "oauthClient")]
        public OAuthLoginClient OAuthClient { get; set; }

        [InvisibleInTestExample]
        public string RedirectUriOnSuccess { get; set; }

        [InvisibleInTestExample]
        public string RedirectUriOnFailure { get; set; }
    }
}