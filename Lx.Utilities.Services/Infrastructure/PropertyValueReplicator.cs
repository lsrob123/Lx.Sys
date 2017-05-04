using System;
using System.Linq;

namespace Lx.Utilities.Services.Infrastructure
{
    public static class PropertyValueReplicator
    {
        public static void Replicate<T>(T fromObject, T toObject, Func<string, bool> isMappedFirstTierProperty = null)
        {
            var properties = typeof (T).GetProperties()
                .Where(x => isMappedFirstTierProperty?.Invoke(x.Name) ?? true)
                .ToList();
            foreach (var property in properties)
                property.SetValue(toObject, property.GetValue(fromObject));
        }

        public static void Replicate<T>(T fromObject, T toObject, params string[] namesOfExcludedFirstTierProperties)
        {
            Replicate(fromObject, toObject, x =>
                namesOfExcludedFirstTierProperties?.All(y => !y.Equals(x, StringComparison.OrdinalIgnoreCase)) ?? true);
        }
    }
}