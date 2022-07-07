namespace WorldIndexesComparer.Domain.Indexes
{
    public class Index
    {
        public long Id { get; set; }
        public Guid CountryId { get; set; }
        public IndexType Type { get; set; }
        public Periodicity Periodicity { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
