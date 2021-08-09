using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model
{
    public class PostType : AlegzaModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("checkable")]
        public string Checkable { get; set; }

        [JsonPropertyName("length")]
        public string Length { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("in_calendar")]
        public string InCalendar { get; set; }

        [JsonPropertyName("deleted_at")]
        public string DeletedAt { get; set; }
    }
}