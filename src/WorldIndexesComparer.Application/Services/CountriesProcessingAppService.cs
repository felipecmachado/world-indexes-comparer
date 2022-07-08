using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using RestCountries.Client;
using RestCountries.Client.Models;
using WorldIndexesComparer.Application.Services.Interfaces;
using WorldIndexesComparer.Domain.Countries;

namespace WorldIndexesComparer.Application.Services
{
    public class CountriesProcessingAppService : ICountriesProcessingAppService
    {
        private readonly ILogger<CountriesProcessingAppService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestCountriesClient _restCountriesClient;

        public CountriesProcessingAppService(ILogger<CountriesProcessingAppService> logger, IUnitOfWork unitOfWork, IRestCountriesClient restCountriesClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _restCountriesClient = restCountriesClient ?? throw new ArgumentNullException(nameof(restCountriesClient));
        }

        public async Task SyncAllCountriesAsync(CancellationToken stoppingToken)
        {
            try
            {
                var countries = await _restCountriesClient.GetAllCountriesAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);

                var existingCountries = await GetExistingCountriesAsync()
                    .ConfigureAwait(continueOnCapturedContext: false);

                foreach (var country in countries)
                {
                    stoppingToken.ThrowIfCancellationRequested();

                    var existingCountry = existingCountries.FirstOrDefault(c => c.CCA3 == country.CCA3);

                    await SaveOrUpdateAsync(country, existingCountry)
                        .ConfigureAwait(continueOnCapturedContext: false);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while synchronizing all countries.");

                throw;
            }
        }

        private async Task<IEnumerable<Country>> GetExistingCountriesAsync()
        {
            var repo = _unitOfWork.Repository<Country>();

            var query = repo.MultipleResultQuery();

            var existingCountries = await repo.SearchAsync(query)
                .ConfigureAwait(continueOnCapturedContext: false);

            return existingCountries;
        }

        private async Task SaveOrUpdateAsync(CountryResult country, Country? existingCountry)
        {
            var repo = _unitOfWork.Repository<Country>();

            if (existingCountry is null)
            {
                var newCountry = Country
                    .New(country.Name.Official, country.CCA2, country.CCA3)
                    .SetPopulation(country.Population);

                repo.Add(newCountry);
            }
            else
            {
                existingCountry.UpdatePopulation(country.Population);

                repo.Update(existingCountry);
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
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
                    _unitOfWork.Dispose();
                }
            }

            _disposed = true;
        }

        #endregion IDisposable Members
    }
}
