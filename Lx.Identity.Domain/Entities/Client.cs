using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Enumerations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities
{
    public class Client : EntityBase, IClient
    {
        public bool Enabled { get; protected set; }

        [Required]
        [StringLength(200)]
        public string ClientId { get; protected set; }

        [Required]
        [StringLength(200)]
        public string ClientName { get; protected set; }

        [StringLength(2000)]
        public string ClientUri { get; protected set; }

        public string LogoUri { get; protected set; }
        public bool RequireConsent { get; protected set; }
        public bool AllowRememberConsent { get; protected set; }
        public bool AllowAccessTokensViaBrowser { get; protected set; }
        public Flows Flow { get; protected set; }
        public bool AllowClientCredentialsOnly { get; protected set; }
        public string LogoutUri { get; protected set; }
        public bool LogoutSessionRequired { get; protected set; }
        public bool RequireSignOutPrompt { get; protected set; }

        public bool AllowAccessToAllScopes { get; protected set; }

        // in seconds
        [Range(0, int.MaxValue)]
        public int IdentityTokenLifetime { get; protected set; }

        [Range(0, int.MaxValue)]
        public int AccessTokenLifetime { get; protected set; }

        [Range(0, int.MaxValue)]
        public int AuthorizationCodeLifetime { get; protected set; }

        [Range(0, int.MaxValue)]
        public int AbsoluteRefreshTokenLifetime { get; protected set; }

        [Range(0, int.MaxValue)]
        public int SlidingRefreshTokenLifetime { get; protected set; }

        public TokenUsage RefreshTokenUsage { get; protected set; }
        public bool UpdateAccessTokenOnRefresh { get; protected set; }

        public TokenExpiration RefreshTokenExpiration { get; protected set; }

        public AccessTokenType AccessTokenType { get; protected set; }

        public bool EnableLocalLogin { get; protected set; }

        public bool IncludeJwtId { get; protected set; }

        public bool AlwaysSendClientClaims { get; protected set; }
        public bool PrefixClientClaims { get; protected set; }

        public bool AllowAccessToAllGrantTypes { get; protected set; }

        [StringLength(100)]
        public string UserProfileOriginator { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull()
        {
            Flow = Flow ?? Flows.ResourceOwner;
            RefreshTokenUsage = RefreshTokenUsage ?? TokenUsage.ReUse;
            RefreshTokenExpiration = RefreshTokenExpiration ?? TokenExpiration.Sliding;
            AccessTokenType = AccessTokenType ?? AccessTokenType.Reference;
        }
    }
}