using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using WorldIndexesComparer.Domain.Countries.Events;
using WorldIndexesComparer.Domain.Indexes.Enums;

namespace WorldIndexesComparer.Application.Coronavirus.EventHandlers
{
    using Index = Domain.Indexes.Index;

    public class CreateCPIWhenCountrySynchronizedEventHandler : INotificationHandler<CountrySynchronizedDomainEvent>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCPIWhenCountrySynchronizedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(CountrySynchronizedDomainEvent notification, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Index>();

            var index = await GetIndexAsync(notification.Country.Id).ConfigureAwait(continueOnCapturedContext: false);

            if (index is null)
            {
                index = Index.New(
                    countryId: notification.Country.Id,
                    ticker: string.Empty,
                    description: string.Empty,
                    type: IndexType.ConsumerPriceIndex);

                repo.Add(index);
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        private async Task<Index> GetIndexAsync(Guid identifier)
        {
            var repo = _unitOfWork.Repository<Index>();

            var query = repo.SingleResultQuery()
                .AndFilter(x => x.CountryId == identifier && x.Type == IndexType.ConsumerPriceIndex);

            var index = await repo.FirstOrDefaultAsync(query)
                .ConfigureAwait(continueOnCapturedContext: false);

            return index;
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
