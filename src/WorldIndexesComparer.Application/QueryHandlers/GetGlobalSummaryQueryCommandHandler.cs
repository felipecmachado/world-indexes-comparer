using EntityFrameworkCore.UnitOfWork.Interfaces;
using WorldIndexesComparer.Application.Queries;
using WorldIndexesComparer.Application.Results;
using WorldIndexesComparer.Domain;
using WorldIndexesComparer.Domain.Coronavirus;

namespace WorldIndexesComparer.Application.QueryHandlers
{
    public class GetGlobalSummaryQueryCommandHandler : IQueryHandler<GetGlobalSummaryQueryCommand, GlobalSummaryResult>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGlobalSummaryQueryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<GlobalSummaryResult> Handle(GetGlobalSummaryQueryCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Summary>();

            var query = repo.MultipleResultQuery();

            var summaries = await repo.SearchAsync(query)
                .ConfigureAwait(continueOnCapturedContext: false);

            var global = new GlobalSummaryResult()
            {
                TotalCases = summaries.Sum(x => x.TotalCases),
                TotalRecovered = summaries.Sum(x => x.TotalRecovered),
                TotalDeaths = summaries.Sum(x => x.TotalDeaths),
                ReferenceDate = summaries.Max(x => x.LastReceivedDate ?? DateTime.MinValue)
            };

            return global;
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
