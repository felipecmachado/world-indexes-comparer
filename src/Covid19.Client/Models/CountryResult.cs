using System.Text.Json.Serialization;

namespace Covid19.Client.Models
{
    public class CountryResult
    {
        public string Country { get; set; }
        public string Slug { get; set; }
        public string ISO2 { get; set; }

    }
}
