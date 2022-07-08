using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using RestCountries.Client;
using RestCountries.Client.Models;
using WorldIndexesComparer.Application.Services.Interfaces;
using WorldIndexesComparer.Domain.Countries;

namespace WorldIndexesComparer.Application.Services
{
    public class CoronavirusDataProcessingAppService : ICoronavirusDataProcessingAppService
    {
        private readonly ILogger<CountriesProcessingAppService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CoronavirusDataProcessingAppService(ILogger<CountriesProcessingAppService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));        }

        public async Task SyncAllCountriesAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
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
