using Lx.Utilities.Contracts.Infrastructure.Attributes;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace Lx.Utilities.Contracts.Authentication.DTOs
{
    public class GetTokensRequest : RequestBase
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonProperty(PropertyName = "oauthLogin")]
        public OAuthLogin OAuthLogin { get; set; }

        [InvisibleInTestExample]
        public string RedirectUriOnSuccess { get; set; }

        [InvisibleInTestExample]
        public string RedirectUriOnFailure { get; set; }
    }
}