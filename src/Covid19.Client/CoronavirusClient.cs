using Covid19.Client.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldIndexesComparer.Common.Http;

namespace Covid19.Client
{
    public class CoronavirusClient : HttpClientBase, ICoronavirusClient
    {
        public readonly IHttpClientFactory _httpClientFactory;

        public CoronavirusClient(IHttpClientFactory httpClientFactory, ILogger<CoronavirusClient> logger)
            : base(logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IList<CountryResult>> GetCountriesSlugs()
        {
            var url = $"countries";

            var response = await GetAsync<IList<CountryResult>>(url)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        public async Task<IList<StatsResult>> GetTotalsByCountry(string slug)
        {
            var url = $"total/country/{slug}";

            var response = await GetAsync<IList<StatsResult>>(url)
                .ConfigureAwait(continueOnCapturedContext: false);

            return response;
        }

        protected override HttpClient CreateHttpClient(ICollection<KeyValuePair<string, string>> headers = null)
            => _httpClientFactory.CreateClient(nameof(ICoronavirusClient));
    }
}
