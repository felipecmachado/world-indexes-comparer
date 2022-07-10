using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using WorldIndexesComparer.Domain.Coronavirus;
using WorldIndexesComparer.Domain.Countries.Events;

namespace WorldIndexesComparer.Application.Coronavirus.EventHandlers
{
    public class CreateSummaryWhenCountrySynchronizedEventHandler : INotificationHandler<CountrySynchronizedDomainEvent>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSummaryWhenCountrySynchronizedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(CountrySynchronizedDomainEvent notification, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Summary>();

            var summary = await GetExistingSummaryAsync(notification.Country.Id).ConfigureAwait(continueOnCapturedContext: false);

            if (summary is null)
            {
                summary = Summary.New(
                    countryId: notification.Country.Id,
                    code: notification.Country.Slug,
                    population: notification.Country.Population);

                repo.Add(summary);
            }
            else
            {
                summary.UpdatePopulation(notification.Country.Population);

                repo.Update(summary);
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        private async Task<Summary> GetExistingSummaryAsync(Guid identifier)
        {
            var repo = _unitOfWork.Repository<Summary>();

            var query = repo.SingleResultQuery()
                .AndFilter(x => x.CountryId == identifier);

            var summary = await repo.FirstOrDefaultAsync(query)
                .ConfigureAwait(continueOnCapturedContext: false);

            return summary;
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
