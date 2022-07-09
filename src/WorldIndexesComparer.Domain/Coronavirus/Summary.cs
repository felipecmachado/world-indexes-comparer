namespace WorldIndexesComparer.Domain.Coronavirus
{
    public class Summary : IAggregateRoot
    {
        public int Id { get; private set; }
        public Guid CountryId { get; private set; }
        public string CountryCode { get; private set; }
        public int Population { get; private set; }
        public DateOnly LastReceivedDate { get; private set; }
        public int TotalCases { get; private set; }
        public int TotalDeaths { get; private set; }
        public int TotalRecovered { get; private set; }

        public int TotalCasesPerMillion => (TotalCases / Population) * 1_000_000;
        public int TotalDeathsPerMillion => (TotalDeaths / Population) * 1_000_000;
        public int TotalRecoveredPerMillion => (TotalRecovered / Population) * 1_000_000;

        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }

        public ICollection<DailyStats> Stats { get; private set; } = new List<DailyStats>();
    }
}
