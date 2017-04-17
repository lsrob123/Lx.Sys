using System;
using System.Collections.Generic;
using Lx.Identity.Contracts.Enumerations;
using Lx.Identity.Contracts.Interfaces;

namespace Lx.Identity.Contracts.DTOs
{
    public class ClientDto : IClient
    {
        public ICollection<ClientRedirectUriDto> RedirectUris { get; set; }
        public ICollection<ClientScopeDto> AllowedScopes { get; set; }
        public ICollection<ClientSecretDto> ClientSecrets { get; set; }
        public string UserProfileOriginator { get; set; }
        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
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
        public int IdentityTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
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
        public DateTimeOffset? TimeCreated { get; set; }
        public DateTimeOffset? TimeModified { get; set; }
        public Guid Key { get; set; }
    }
}