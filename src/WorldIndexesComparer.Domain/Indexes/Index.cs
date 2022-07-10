using WorldIndexesComparer.Domain.Indexes.Enums;

namespace WorldIndexesComparer.Domain.Indexes
{
    public class Index
    {
        public long Id { get; private set; }
        public Guid? CountryId { get; private set; }
        public string Ticker { get; private set; }
        public string Description { get; private set; }
        public IndexType Type { get; private set; }
        public Periodicity Periodicity { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }

        public ICollection<IndexValue> Values { get; set; } = new List<IndexValue>();

        public static Index New(Guid countryId, string ticker, string description, IndexType type)
        {
            var index = new Index()
            {
                CountryId = countryId,
                Ticker = ticker,
                Description = description,
                Type = type,
                Periodicity = Periodicity.Unspecified
            };

            return index;
        }
    }
}
