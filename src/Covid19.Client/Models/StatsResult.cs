namespace Covid19.Client.Models
{
    public class StatsResult
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public int Active { get; set; }
        public DateTime Date { get; set; }
    }
}
