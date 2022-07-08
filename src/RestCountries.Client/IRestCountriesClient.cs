using RestCountries.Client.Models;

namespace RestCountries.Client
{
    public interface IRestCountriesClient
    {
        Task<CountryResult> GetCountryAsync(string name);
        Task<IList<CountryResult>> GetAllCountriesAsync();
    }
}
