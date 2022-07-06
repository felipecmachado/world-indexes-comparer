using RestCountries.Client.Models;

namespace RestCountries.Client
{
    public interface IRestCountriesClient
    {
        Task<Country> GetCountry(string name);
        Task<IList<Country>> GetAllCountries();
    }
}
