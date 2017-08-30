using System;

namespace Lx.Utilities.Contracts.Serialization
{
    [Flags]
    public enum SerializationOptions
    {
        Default = 0,
        UseFullContractResolver = 1,
        IgnoreSelfReferencedProperties = 2
    }
}