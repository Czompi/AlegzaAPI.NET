using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model
{
    public class PersonProduct : ProductType
    {

        [JsonPropertyName("type")]
        public ProductType Type { get; set; }

        [JsonPropertyName("provider")]
        public ProductProvider Provider { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("housing_state")]
        public int HousingState { get; set; }
    }
}