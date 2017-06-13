using Lx.Identity.Contracts.DTOs;
using Lx.Utilities.Contracts.Configuration;
using Lx.Utilities.Services.Mapping.AutoMapper;

namespace Lx.Identity.ID3.Mapping
{
    public static class Maps
    {
        [Preconfiguration]
        public static void Add()
        {
            MappingService.AddMaps(
                new MapSetting<ClientScopeDto, string>(exp => exp.ConstructUsing(x => ((ClientScopeDto) x).Scope)),
                new MapSetting<ClientRedirectUriDto, string>(
                    exp => exp.ConstructUsing(x => ((ClientRedirectUriDto) x).Uri))
            );
        }
    }
}