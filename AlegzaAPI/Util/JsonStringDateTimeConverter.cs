using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Util
{
    internal class JsonStringDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var data = reader.GetString();
            try
            {
                return DateTime.ParseExact(data, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffff'Z'", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return DateTime.ParseExact(data, "yyyy'-'MM'-'dd' 'HH':'mm':'ss", CultureInfo.InvariantCulture);
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"));
        }
    }
}