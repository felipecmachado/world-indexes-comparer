using Microsoft.Extensions.Logging;
using RestCountries.Client.Models;
using WorldIndexesComparer.Common.Http;

namespace RestCountries.Client
{
    /// <summary>
    /// https://restcountries.com/#api-endpoints-v3-all
    /// https://github.com/mledoze/countries
    /// </summary>
    public class RestCountriesClient : HttpClientBase, IRestCountriesClient
    {
        public readonly IHttpClientFactory _httpClientFactory;

        public RestCountriesClient(IHttpClientFactory httpClientFactory, ILogger<RestCountriesClient> logger)
            : base(logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IList<CountryResult>> GetAllCountriesAsync()
        {
            var url = $"all";

            var response = await GetAsync<IList<CountryResult>>(url)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        public async Task<CountryResult> GetCountry(string name)
        {
            var url = $"name/{name}";

            var response = await GetAsync<IList<CountryResult>>(url)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response.FirstOrDefault();
        }

        protected override HttpClient CreateHttpClient(ICollection<KeyValuePair<string, string>> headers = null)
            => _httpClientFactory.CreateClient(nameof(IRestCountriesClient));
    }
}
