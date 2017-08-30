using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lx.Utilities.Contracts.Caching
{
    public interface ICacheWithHashes : ICache
    {
        Task HashSetAsync(string hashKey, string itemName, string itemValue);
        Task HashSetAsync(string hashKey, IDictionary<string, string> nameValues);
        Task HashSetAsync(string hashKey, params KeyValuePair<string, string>[] nameValues);
        Task HashDeleteAsync(string hashKey, string fieldName = null);

        IDictionary<string, string> HashGet(string hashKey, IEnumerable<string> nameEnumerable = null,
            params string[] individuallySuppliedNames);

        string HashGet(string hashKey, string fieldName);
    }
}