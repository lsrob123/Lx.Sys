using Lx.Utilities.Contracts.Infrastructure.Attributes;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Utilities.Contracts.Authentication.DTOs
{
    public class RefreshTokensRequest : RequestBase, IHasOAuthLoginClient
    {
        public string RefreshToken { get; set; }

        [InvisibleInTestExample]
        public string RedirectUriOnSuccess { get; set; }

        [InvisibleInTestExample]
        public string RedirectUriOnFailure { get; set; }

        [JsonProperty(PropertyName = "oauthClient")]
        public OAuthLoginClient OAuthClient { get; set; }
    }
}