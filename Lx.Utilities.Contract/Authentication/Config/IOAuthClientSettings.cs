using System;

namespace Lx.Utilities.Contract.Authentication.Config
{
    public interface IOAuthClientSettings
    {
        string DefaultClientId { get; }
        string DefaultClientSecret { get; }
        string DefaultScopes { get; }
        string DefaultGrantType { get; }
        TimeSpan? AccessTokenValidationResultLifeSpan { get; }
        string UserInfoEndpointAbsolutePath { get; }
    }
}