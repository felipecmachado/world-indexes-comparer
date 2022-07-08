namespace RestCountries.Client.Models
{
    public class CountryResult
    {
        public CountryNameResult Name { get; set; }
        public string CCA2 { get; set; }
        public string CCA3 { get; set; }
        public int Population { get; set; }
    }
}
