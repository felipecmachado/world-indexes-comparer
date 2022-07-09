using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using WorldIndexesComparer.Application.Coronavirus.Services.Interfaces;

namespace WorldIndexesComparer.Application.Coronavirus.Services
{
    public class CoronavirusDataProcessingAppService : ICoronavirusDataProcessingAppService
    {
        private readonly ILogger<CoronavirusDataProcessingAppService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CoronavirusDataProcessingAppService(ILogger<CoronavirusDataProcessingAppService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));        
        }

        public Task RefreshDataAsync(CancellationToken stoppingToken)
        {
            // get X countries to sync based on the most outdated and population ordered

            // foreach var country in countries
            {
                // get history of cases
                // get history of deaths
                // get history of recovered

                // update summary

                // save changes
            }

            return Task.CompletedTask;
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
