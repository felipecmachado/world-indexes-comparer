namespace RestCountries.Client.Models
{
    public class Country
    {
        public CountryName Name { get; set; }
        public string CCA2 { get; set; }
        public string CCA3 { get; set; }
        public int Population { get; set; }
    }
}
