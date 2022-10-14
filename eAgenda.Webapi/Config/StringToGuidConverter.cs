using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eAgenda.Webapi.Config
{
    public class StringToGuidConverter :JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (Guid.TryParse(value, out Guid result))
                return result;
            
            return Guid.Empty;
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
       
    }
}
