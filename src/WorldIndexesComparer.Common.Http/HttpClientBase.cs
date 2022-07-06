using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WorldIndexesComparer.Common.Http
{
    public abstract class HttpClientBase
    {
        private readonly ILogger _logger;
        private readonly JsonSerializerOptions _options;

        public HttpClientBase(ILogger logger = null)
        {
            _logger = logger;
            _options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        protected abstract HttpClient CreateHttpClient(ICollection<KeyValuePair<string, string>> headers = null);

        protected async Task<TResponse> GetAsync<TResponse>(string uri, ICollection<KeyValuePair<string, string>> headers = null)
        {
            var client = CreateHttpClient(headers);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await client.SendAsync(request).ConfigureAwait(continueOnCapturedContext: false);

            await HandleResponseAsync(response).ConfigureAwait(continueOnCapturedContext: false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
            var result = FromJson<TResponse>(content);

            return result;
        }

        protected T FromJson<T>(string json, JsonSerializerOptions options = null)
        {
            var objectResult = (T)FromJson(json, typeof(T), options);

            return objectResult;
        }

        protected object FromJson(string json, Type type, JsonSerializerOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            var optionsInternal = options ?? _options;

            var objectResult = JsonSerializer.Deserialize(json, type, optionsInternal);

            return objectResult;
        }

        private async Task HandleResponseAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new AuthenticationException(content);
                }

                throw new HttpRequestException(content, inner: null, statusCode: response.StatusCode);
            }
        }
    }
}