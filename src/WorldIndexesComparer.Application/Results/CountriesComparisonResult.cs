namespace WorldIndexesComparer.Application.Results
{
    public class CountriesComparisonResult
    {
        public IList<CountryStatsResult> Countries { get; set; }

        public CountriesComparisonResult()
        {
            Countries = new List<CountryStatsResult>();
        }
    }

    public class CountryStatsResult
    {
        public string CountryName { get; set; }
        public int Population { get; set; }
        public int Year { get; set; }
        public decimal CoronavirusDeathsPerMillion { get; set; }
        public decimal ConsumerPriceIndex { get; set; }
    }
}
