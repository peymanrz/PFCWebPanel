using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace PersianFiberWeb.ExtensionList
{

    public partial class GettingExtensionList
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExtensionS> Results { get; set; }
    }

    public partial class ExtensionS
    {
        [JsonProperty("dial", NullValueHandling = NullValueHandling.Ignore)]
        public string Dial { get; set; }

        [JsonProperty("secret", NullValueHandling = NullValueHandling.Ignore)]
        public string Secret { get; set; }

        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public string Context { get; set; }

        [JsonProperty("extension", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Extension { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("tech", NullValueHandling = NullValueHandling.Ignore)]
        public string Tech { get; set; }

        [JsonProperty("voicemail", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Voicemail { get; set; }

        [JsonProperty("followme", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Followme { get; set; }

        [JsonProperty("dictation", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Dictation { get; set; }
    }

    public partial class GettingExtensionList
    {
        public static GettingExtensionList FromJson(string json) => JsonConvert.DeserializeObject<GettingExtensionList>(json, PersianFiberWeb.ExtensionList.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GettingExtensionList self) => JsonConvert.SerializeObject(self, PersianFiberWeb.ExtensionList.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
