namespace Lx.Utilities.Contracts.Serialization
{
    public interface IClaimRelatedJsonDeserializer
    {
        T Deserialize<T>(string serialized);
    }
}