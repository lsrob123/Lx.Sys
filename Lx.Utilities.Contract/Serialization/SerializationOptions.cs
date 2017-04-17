using System;

namespace Lx.Utilities.Contract.Serialization
{
    [Flags]
    public enum SerializationOptions
    {
        Default = 0,
        UseFullContractResolver = 1,
        IgnoreSelfReferencedProperties = 2
    }
}