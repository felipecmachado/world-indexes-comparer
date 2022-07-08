namespace WorldIndexesComparer.Domain.Indexes
{
    public class IndexValue
    {
        public long Id { get; private set; }
        public long IndexId { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
