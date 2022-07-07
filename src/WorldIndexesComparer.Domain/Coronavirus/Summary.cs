namespace WorldIndexesComparer.Domain.Coronavirus
{
    public class Summary
    {
        public Guid CountryId { get; set; }
        public string CountryCode { get; set; }
        public DateOnly LastReceivedDate { get; set; }
        public int TotalCases { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalRecovered { get; set; }
        public int TotalCasesPerMillion { get; set; }
        public int TotalDeathsPerMillion { get; set; }
        public int TotalRecoveredPerMillion { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
