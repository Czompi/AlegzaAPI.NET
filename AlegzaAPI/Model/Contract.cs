using AlegzaCRM.AlegzaAPI.Util;
using System;
using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model;

public class Contract : AlegzaModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; set; }

    [JsonPropertyName("time")]
    [JsonConverter(typeof(JsonStringDateTimeConverter))]
    public DateTime? Time { get; set; }

    [JsonPropertyName("technical_start")]
    [JsonConverter(typeof(JsonStringDateTimeConverter))]
    public DateTime? TechnicalStart { get; set; }

    [JsonPropertyName("bond_number")]
    public string BondNumber { get; set; }

    [JsonPropertyName("person")]
    public int Person { get; set; }

    [JsonPropertyName("product")]
    public int Product { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("post")]
    public string Post { get; set; }
}
