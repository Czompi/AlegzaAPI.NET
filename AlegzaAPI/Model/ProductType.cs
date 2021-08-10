using AlegzaCRM.AlegzaAPI.Util;
using System;
using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model
{
    public class ProductType : AlegzaModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(JsonStringDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        [JsonConverter(typeof(JsonStringDateTimeConverter))]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("deleted_at")]
        [JsonConverter(typeof(JsonStringDateTimeConverter))]
        public DateTime? DeletedAt { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}