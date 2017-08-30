using System;
using Lx.Utilities.Contracts.Infrastructure.Common;

namespace Lx.Utilities.Contracts.Serialization
{
    /// <summary>
    ///     For implementation of serializer
    /// </summary>
    public interface ISerializer
    {
        bool CanDeserialize(string strInput);

        string Serialize(object anyObject, Casing casing = Casing.Default,
            SerializationOptions serializationOptions = SerializationOptions.IgnoreSelfReferencedProperties);

        T Deserialize<T>(string serialized, Casing casing = Casing.Default,
            SerializationOptions serializationOptions =
                SerializationOptions.UseFullContractResolver | SerializationOptions.IgnoreSelfReferencedProperties);

        object Deserialize(string serialized, Type type);
    }
}