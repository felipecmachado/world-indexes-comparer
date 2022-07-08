namespace WorldIndexesComparer.Domain.Coronavirus
{
    public class Summary
    {
        public int Id { get; private set; }
        public Guid CountryId { get; set; }
        public string CountryCode { get; set; }
        public DateOnly LastReceivedDate { get; private set; }
        public int TotalCases { get; private set; }
        public int TotalDeaths { get; private set; }
        public int TotalRecovered { get; private set; }
        public int TotalCasesPerMillion { get; private set; }
        public int TotalDeathsPerMillion { get; private set; }
        public int TotalRecoveredPerMillion { get; private set; }
        
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }

        public ICollection<DailyStats> Stats { get; private set; } = new List<DailyStats>();
    }
}
