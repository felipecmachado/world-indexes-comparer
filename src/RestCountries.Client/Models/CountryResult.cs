using System.Text.Json.Serialization;

namespace RestCountries.Client.Models
{
    public class CountryResult
    {
        public CountryNameResult Name { get; set; }

        [JsonPropertyName("cca2")]
        public string CCA2 { get; set; }

        [JsonPropertyName("cca3")]
        public string CCA3 { get; set; }

        public int Population { get; set; }
    }
}
