namespace WorldIndexesComparer.Domain.Coronavirus
{
    public class Summary : Entity, IAggregateRoot
    {
        public int Id { get; private set; }
        public Guid CountryId { get; private set; }
        public string Slug { get; private set; }
        public int Population { get; private set; }
        public DateOnly? LastReceivedDate { get; private set; }
        public int TotalCases { get; private set; }
        public int TotalDeaths { get; private set; }
        public int TotalRecovered { get; private set; }

        public int TotalCasesPerMillion => (TotalCases / Population) * 1_000_000;
        public int TotalDeathsPerMillion => (TotalDeaths / Population) * 1_000_000;
        public int TotalRecoveredPerMillion => (TotalRecovered / Population) * 1_000_000;

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        public ICollection<DailyStats> Stats { get; private set; } = new List<DailyStats>();

        public static Summary New(Guid countryId, string code, int population)
        {
            var summary = new Summary()
            {
                CountryId = countryId,
                Slug = code,
                Population = population,
                TotalCases = 0,
                TotalDeaths = 0,
                TotalRecovered= 0,
                CreatedAt = DateTime.UtcNow
            };

            return summary;
        }

        public void UpdateSummary(DateOnly date, int totalCases, int totalDeaths, int totalRecovered)
        {
            if (date < LastReceivedDate)
            {
                throw new ArgumentException("Date cannot be earlier than the last received date.");
            }

            LastReceivedDate = date;
            TotalCases = totalCases;
            TotalDeaths = totalDeaths;
            TotalRecovered = totalRecovered;
            LastUpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePopulation(int population)
        {
            if (population < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(population));
            }

            Population = population;
        }
    }
}
