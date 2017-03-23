using System;
using System.Security.Claims;
using Lx.Utilities.Contract.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lx.Utilities.Services.Serialization {
    public class ClaimConverter : JsonConverter {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(Claim);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            Newtonsoft.Json.JsonSerializer serializer) {
            var jo = JObject.Load(reader);
            var type = (string) jo["Type"];
            var value = (string) jo["Value"];
            var valueType = (string) jo["ValueType"];
            var issuer = (string) jo["Issuer"];
            var originalIssuer = (string) jo["OriginalIssuer"];
            return new Claim(type, value, valueType, issuer, originalIssuer);
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer) {}
    }

    public class ClaimRelatedJsonDeserializer : IClaimRelatedJsonDeserializer {
        public T Deserialize<T>(string serialized) {
            var o2 = JsonConvert.DeserializeObject<T>(serialized, new ClaimConverter());
            return o2;
        }
    }
}