using System.Text.Json.Serialization;

namespace RestCountries.Client.Models
{
    public class CountryNameResult
    {
        [JsonPropertyName("common")]
        public string Common { get; set; }
        [JsonPropertyName("official")]
        public string Official { get; set; }
    }
}
