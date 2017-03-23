using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class Client : EntityBase {
        public bool Enabled { get; set; }

        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }

        [Required]
        [StringLength(200)]
        public string ClientName { get; set; }

        [StringLength(2000)]
        public string ClientUri { get; set; }

        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public Flows Flow { get; set; }
        public bool AllowClientCredentialsOnly { get; set; }
        public string LogoutUri { get; set; }
        public bool LogoutSessionRequired { get; set; }
        public bool RequireSignOutPrompt { get; set; }

        public bool AllowAccessToAllScopes { get; set; }

        // in seconds
        [Range(0, int.MaxValue)]
        public int IdentityTokenLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public int AccessTokenLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public int AuthorizationCodeLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public int AbsoluteRefreshTokenLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public int SlidingRefreshTokenLifetime { get; set; }

        public TokenUsage RefreshTokenUsage { get; set; }
        public bool UpdateAccessTokenOnRefresh { get; set; }

        public TokenExpiration RefreshTokenExpiration { get; set; }

        public AccessTokenType AccessTokenType { get; set; }

        public bool EnableLocalLogin { get; set; }

        public bool IncludeJwtId { get; set; }

        public bool AlwaysSendClientClaims { get; set; }
        public bool PrefixClientClaims { get; set; }

        public bool AllowAccessToAllGrantTypes { get; set; }

        [StringLength(100)]
        public string Context { get; set; }

        [StringLength(100)]
        public string Group { get; set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {
            Flow = Flow ?? Flows.ResourceOwner;
            RefreshTokenUsage = RefreshTokenUsage ?? TokenUsage.ReUse;
            RefreshTokenExpiration = RefreshTokenExpiration ?? TokenExpiration.Sliding;
            AccessTokenType = AccessTokenType ?? AccessTokenType.Reference;
        }
    }
}