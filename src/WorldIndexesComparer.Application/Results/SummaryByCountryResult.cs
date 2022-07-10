namespace WorldIndexesComparer.Application.Results
{
    public class SummaryByCountryResult
    {
        public string CountryName { get; set; }
        public int Population { get; set; }
        public DateTime? LastReceivedDate { get; set; }
        public int TotalCases { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalRecovered { get; set; }
        public decimal TotalCasesPerMillion { get; set; }
        public decimal TotalDeathsPerMillion { get; set; }
        public decimal TotalRecoveredPerMillion { get; set; }

    }
}
