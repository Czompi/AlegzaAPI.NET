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
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public IDictionary<string, ICollection<string>>? Errors { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}