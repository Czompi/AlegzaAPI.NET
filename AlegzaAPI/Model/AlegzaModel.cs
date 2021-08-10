using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AlegzaCRM.AlegzaAPI.Model
{
    /// <summary>
    /// Ez az osztály az APIException helyett van
    /// </summary>
    public class AlegzaModel
    {
        [JsonPropertyName("error")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("errors")]
        public IDictionary<string, ICollection<string>>? Errors { get; set; }
    }
}