using Newtonsoft.Json;
using System;

namespace PixelWizards.PackageUtil
{

    public class DependencySerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = value as DependencyList;
            writer.WriteStartObject();
            foreach (var entry in list.entries)
            {
                writer.WritePropertyName(entry.name);
                serializer.Serialize(writer, entry.version);
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DependencySerializer).IsAssignableFrom(objectType);
        }
    }
}