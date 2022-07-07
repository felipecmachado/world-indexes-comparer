
namespace WorldIndexesComparer.Domain.Coronavirus
{
    public class DailyStats
    {
        public Guid CountryId { get; set; }
        public DateOnly Date { get; set; }
        public int Cases { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
