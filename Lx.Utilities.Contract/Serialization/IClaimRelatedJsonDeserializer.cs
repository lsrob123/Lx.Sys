namespace Lx.Utilities.Contract.Serialization {
    public interface IClaimRelatedJsonDeserializer {
        T Deserialize<T>(string serialized);
    }
}