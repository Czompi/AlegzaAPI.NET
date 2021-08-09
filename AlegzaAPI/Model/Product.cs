using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model
{
    public class Product : ProductType
    {

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}