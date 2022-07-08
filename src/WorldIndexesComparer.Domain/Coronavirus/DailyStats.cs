
namespace WorldIndexesComparer.Domain.Coronavirus
{
    public class DailyStats
    {
        public long Id { get; private set; }
        public int SummaryId { get; private set; }
        public DateOnly Date { get; private set; }
        public int Cases { get; private set; }
        public int Deaths { get; private set; }
        public int Recovered { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
    }
}
