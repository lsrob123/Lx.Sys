using System.Linq;
using Lx.Utilities.Contracts.Web;
using Lx.Utilities.Services.Serialization;

namespace Lx.Utilities.Services.Web
{
    public static class FormPostResultExtensions
    {
        public static T ToObject<T>(this FormPostResult result, string dataFieldName) where T : new()
        {
            var fieldValue = result?.Fields?.GetValues(dataFieldName)?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(fieldValue))
                return default(T);

            var serializer = new JsonSerializer();
            var data = serializer.Deserialize<T>(fieldValue);
            return data;
        }
    }
}