using MediatR;
using Microsoft.Extensions.Logging;
using RestCountries.Client;
using WorldIndexesComparer.Application.Countries.Services.Interfaces;
using WorldIndexesComparer.Domain.Countries.Commands;

namespace WorldIndexesComparer.Application.Countries.Services
{
    public class CountriesProcessingAppService : ICountriesProcessingAppService
    {
        private readonly ILogger<CountriesProcessingAppService> _logger;
        private readonly IRestCountriesClient _restCountriesClient;
        private readonly IMediator _mediator;

        public CountriesProcessingAppService(
            ILogger<CountriesProcessingAppService> logger, 
            IRestCountriesClient restCountriesClient,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restCountriesClient = restCountriesClient ?? throw new ArgumentNullException(nameof(restCountriesClient));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task SyncAllCountriesAsync(CancellationToken stoppingToken)
        {
            try
            {
                var countries = await _restCountriesClient.GetAllCountriesAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);

                foreach (var country in countries)
                {
                    stoppingToken.ThrowIfCancellationRequested();

                    var command = new SynchronizeCountryCommand()
                    {
                        Name = country.Name.Common,
                        OfficialName = country.Name.Official,
                        CCA2 = country.CCA2,
                        CCA3 = country.CCA3,
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
