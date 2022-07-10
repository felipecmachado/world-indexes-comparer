namespace WorldIndexesComparer.Application.Results
{
    public class GlobalSummaryResult
    {
        public DateTime ReferenceDate { get; set; }
        public int TotalCases { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalRecovered { get; set; }
    }
}
