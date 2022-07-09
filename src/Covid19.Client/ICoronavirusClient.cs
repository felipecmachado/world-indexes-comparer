using Covid19.Client.Models;

namespace Covid19.Client
{
    public interface ICoronavirusClient
    {
        Task<IList<CountryResult>> GetCountriesSlugs();
        Task<IList<StatsResult>> GetTotalsByCountry(string slug);
    }
}
