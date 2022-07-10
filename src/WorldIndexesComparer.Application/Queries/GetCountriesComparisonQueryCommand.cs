using WorldIndexesComparer.Application.Results;
using WorldIndexesComparer.Domain;

namespace WorldIndexesComparer.Application.Queries
{
    public class GetCountriesComparisonQueryCommand : IQuery<CountriesComparisonResult>
    {
        public GetCountriesComparisonQueryCommand(string[] countries) => Countries = countries;

        public string[] Countries { get; private set; }
    }
}
