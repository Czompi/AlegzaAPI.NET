using AlegzaCRM.AlegzaAPI.Util;
using System;
using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model
{
    public class Post : AlegzaModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("person")]
        public int Person { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("post_timestamp")]
        [JsonConverter(typeof(JsonStringDateTimeConverter))]
        public DateTime? PostTimestamp { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("success")]
        public string Success { get; set; }

        [JsonPropertyName("deleted_at")]
        public string DeletedAt { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }
    }
}