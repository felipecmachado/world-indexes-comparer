using RestCountries.Client.Models;

namespace RestCountries.Client
{
    public interface IRestCountriesClient
    {
        Task<CountryResult> GetCountry(string name);
        Task<IList<CountryResult>> GetAllCountriesAsync();
    }
}
