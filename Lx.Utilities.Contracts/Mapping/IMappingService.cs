using System;

namespace Lx.Utilities.Contracts.Mapping
{
    public interface IMappingService
    {
        TDestination Map<TDestination>(object source, Func<string, bool> isMappedFirstTierProperty = null);
        TDestination Map<TSource, TDestination>(TSource source, Func<string, bool> isMappedFirstTierProperty = null);
    }
}