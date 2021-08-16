using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model;

public class Response : AlegzaModel
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
}
