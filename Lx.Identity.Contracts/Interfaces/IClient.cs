using System;
using Lx.Identity.Contracts.Enumerations;
using Lx.Shared.All.Identity.Interfaces;

namespace Lx.Identity.Contracts.Interfaces {
    public interface IClient : IHasUserProfileOriginator {
        bool Enabled { get; }
        string ClientId { get; }
        string ClientName { get; }
        string ClientUri { get; }
        string LogoUri { get; }
        bool RequireConsent { get; }
        bool AllowRememberConsent { get; }
        bool AllowAccessTokensViaBrowser { get; }
        Flows Flow { get; }
        bool AllowClientCredentialsOnly { get; }
        string LogoutUri { get; }
        bool LogoutSessionRequired { get; }
        bool RequireSignOutPrompt { get; }
        bool AllowAccessToAllScopes { get; }
        int IdentityTokenLifetime { get; }
        int AccessTokenLifetime { get; }
        int AuthorizationCodeLifetime { get; }
        int AbsoluteRefreshTokenLifetime { get; }
        int SlidingRefreshTokenLifetime { get; }
        TokenUsage RefreshTokenUsage { get; }
        bool UpdateAccessTokenOnRefresh { get; }
        TokenExpiration RefreshTokenExpiration { get; }
        AccessTokenType AccessTokenType { get; }
        bool EnableLocalLogin { get; }
        bool IncludeJwtId { get; }
        bool AlwaysSendClientClaims { get; }
        bool PrefixClientClaims { get; }
        bool AllowAccessToAllGrantTypes { get; }
        DateTimeOffset? TimeCreated { get; }
        DateTimeOffset? TimeModified { get; }
        Guid Key { get; }
    }
}