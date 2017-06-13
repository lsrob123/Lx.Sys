using Lx.Utilities.Contracts.Authentication.DTOs;
using Lx.Utilities.Contracts.Configuration;
using Lx.Utilities.Services.Authentication;

namespace Lx.Utilities.Services.Mapping.AutoMapper
{
    public static class Maps
    {
        [Preconfiguration]
        public static void Add()
        {
            MappingService.AddMaps(
                new MapSetting<OAuthClientSettings, OAuthLoginClient>(exp => exp
                    .ConstructUsing(x => new OAuthLoginClient
                    {
                        ClientId = ((OAuthClientSettings) x).DefaultClientId,
                        ClientSecret = ((OAuthClientSettings) x).DefaultClientSecret
                    })),
                new MapSetting<OAuthClientSettings, OAuthLogin>(exp => exp
                    .ConstructUsing(x => new OAuthLogin
                    {
                        ClientId = ((OAuthClientSettings) x).DefaultClientId,
                        ClientSecret = ((OAuthClientSettings) x).DefaultClientSecret,
                        Scopes = ((OAuthClientSettings) x).DefaultScopes,
                        GrantType = ((OAuthClientSettings) x).DefaultGrantType
                    }))
            );
        }
    }
}