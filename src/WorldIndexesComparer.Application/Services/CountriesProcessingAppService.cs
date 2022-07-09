using Covid19.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using RestCountries.Client;
using WorldIndexesComparer.Application.Services.Interfaces;
using WorldIndexesComparer.Domain.Countries.Commands;

namespace WorldIndexesComparer.Application.Services
{
    public class CountriesProcessingAppService : ICountriesProcessingAppService
    {
        private readonly ILogger<CountriesProcessingAppService> _logger;
        private readonly IRestCountriesClient _restCountriesClient;
        private readonly ICoronavirusClient _coronavirusClient;
        private readonly IMediator _mediator;

        public CountriesProcessingAppService(
            ILogger<CountriesProcessingAppService> logger, 
            IRestCountriesClient restCountriesClient,
            ICoronavirusClient coronavirusClient,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restCountriesClient = restCountriesClient ?? throw new ArgumentNullException(nameof(restCountriesClient));
            _coronavirusClient = coronavirusClient ?? throw new ArgumentNullException(nameof(coronavirusClient));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task SyncAllCountriesAsync(CancellationToken stoppingToken)
        {
            try
            {
                var countries = await _restCountriesClient.GetAllCountriesAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);

                var slugs = await _coronavirusClient.GetCountriesSlugs()
                    .ConfigureAwait(continueOnCapturedContext: false);

                foreach (var country in countries)
                {
                    stoppingToken.ThrowIfCancellationRequested();

                    var slug = slugs.FirstOrDefault(x => x.ISO2 == country.CCA2)?.Slug;

                    var command = new SynchronizeCountryCommand()
                    {
                        Name = country.Name.Common,
                        OfficialName = country.Name.Official,
                        CCA2 = country.CCA2,
                        CCA3 = country.CCA3,
                        Slug = slug,
                        Population = country.Population
                    };

                    var response = await _mediator.Send(command).ConfigureAwait(continueOnCapturedContext: false);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while synchronizing all countries.");

                throw;
            }
        }

        #region IDisposable Members

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }
            }

            _disposed = true;
        }

        #endregion IDisposable Members
    }
}
