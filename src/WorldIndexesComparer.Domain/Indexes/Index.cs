using WorldIndexesComparer.Domain.Countries;

namespace WorldIndexesComparer.Domain.Indexes
{
    public class Index
    {
        public long Id { get; private set; }
        public Guid? CountryId { get; private set; }
        public string Name { get; private set; }
        public IndexType Type { get; private set; }
        public Periodicity Periodicity { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }

        public ICollection<IndexValue> Values { get; set; } = new List<IndexValue>();
    }
}
