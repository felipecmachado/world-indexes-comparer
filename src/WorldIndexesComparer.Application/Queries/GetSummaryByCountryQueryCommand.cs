using WorldIndexesComparer.Application.Results;
using WorldIndexesComparer.Domain;

namespace WorldIndexesComparer.Application.Queries
{
    public class GetSummaryByCountryQueryCommand : IQuery<SummaryByCountryResult>
    {
        public GetSummaryByCountryQueryCommand(string country)
        {
            Country = country;
        }

        public string Country { get; private set; }
    }
}
