using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using WorldIndexesComparer.Domain;
using WorldIndexesComparer.Domain.Coronavirus;
using WorldIndexesComparer.Domain.Coronavirus.Commands;
using WorldIndexesComparer.Domain.Countries;
using WorldIndexesComparer.Domain.Countries.Commands;
using WorldIndexesComparer.Infrastructure.Extensions;

namespace WorldIndexesComparer.Application.Countries.CommandHandlers
{
    public class UpdateCoronavirusSummaryCommandHandler : ICommandHandler<UpdateCoronavirusSummaryCommand, bool>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public UpdateCoronavirusSummaryCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(UpdateCoronavirusSummaryCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Summary>();

            var summary = request.Summary;

            summary.UpdateSummary(request.Date, request.Active, request.Deaths, request.Recovered);

            repo.Update(summary);
  
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);

            await _mediator.DispatchDomainEventsAsync(summary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);

            return true;
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
