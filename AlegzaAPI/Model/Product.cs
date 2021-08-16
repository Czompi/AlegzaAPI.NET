using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model;

public class Product : ProductType
{

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("provider")]
    public int Provider { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }

    [JsonPropertyName("housing_state")]
    public int HousingState { get; set; }
}
