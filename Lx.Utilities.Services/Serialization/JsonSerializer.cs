using System;
using Lx.Utilities.Contracts.Infrastructure.Common;
using Lx.Utilities.Contracts.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

// ReSharper disable SwitchStatementMissingSomeCases

namespace Lx.Utilities.Services.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public bool CanDeserialize(string strInput)
        {
            strInput = strInput.Trim();

            if ((!strInput.StartsWith("{") || !strInput.EndsWith("}")) &&
                (!strInput.StartsWith("[") || !strInput.EndsWith("]")))
                return false; //For array 

            try
            {
                var obj = JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException)
            {
                return false; //Exception in parsing json
            }
            catch (Exception)
            {
                return false; //some other exception
            }
        }

        public string Serialize(object anyObject, Casing casing = Casing.Default,
            SerializationOptions serializationOptions = SerializationOptions.IgnoreSelfReferencedProperties)
        {
            var settings = GetJsonSerializerSettings(serializationOptions, casing);
            var json = JsonConvert.SerializeObject(anyObject, settings);
            return json;
        }

        public T Deserialize<T>(string serialized, Casing casing = Casing.Default,
            SerializationOptions serializationOptions =
                SerializationOptions.UseFullContractResolver | SerializationOptions.IgnoreSelfReferencedProperties)
        {
            if (string.IsNullOrWhiteSpace(serialized))
                return default(T);

            var settings = GetJsonSerializerSettings(serializationOptions, casing);
            var targetObject = JsonConvert.DeserializeObject<T>(serialized, settings);
            return targetObject;
        }

        public object Deserialize(string serialized, Type type)
        {
            var deserialized = JsonConvert.DeserializeObject(serialized, type);
            return deserialized;
        }

        protected virtual JsonSerializerSettings GetJsonSerializerSettings(SerializationOptions serializationOptions,
            Casing serializationCasing)
        {
            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            if (serializationOptions.HasFlag(SerializationOptions.IgnoreSelfReferencedProperties))
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            if (serializationOptions.HasFlag(SerializationOptions.UseFullContractResolver))
                switch (serializationCasing)
                {
                    case Casing.Snake:
                        settings.ContractResolver = new SnakeCaseFullContractResolver();
                        break;
                    case Casing.Camel:
                        settings.ContractResolver = new CamelCaseFullContractResolver();
                        break;
                    default:
                        settings.ContractResolver = new FullContractResolver();
                        break;
                }
            else
                switch (serializationCasing)
                {
                    case Casing.Snake:
                        settings.ContractResolver = new SnakeCasePropertyNameContractResolver();
                        break;
                    case Casing.Camel:
                        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        break;
                    default:
                        settings.ContractResolver = new DefaultContractResolver();
                        break;
                }

            return settings;
        }
    }
}