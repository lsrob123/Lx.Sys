using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;

namespace Lx.Utilities.Services.Serialization
{
    public class SnakeCasePropertyNameContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            // (Preceded by a lowercase character or digit) (a capital) => The character prefixed with an underscore
            var result = Regex.Replace(propertyName, @"([a-z0-9])([A-Z])", "$1_$2").ToLowerInvariant();
            return result;
        }
    }
}