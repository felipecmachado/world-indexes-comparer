using EntityFrameworkCore.UnitOfWork.Interfaces;
using WorldIndexesComparer.Application.Results;
using WorldIndexesComparer.Application.Queries;
using WorldIndexesComparer.Domain;
using WorldIndexesComparer.Domain.Coronavirus;

namespace WorldIndexesComparer.Application.QueryHandlers
{
    public class GetSummaryByCountryQueryCommandHandler : IQueryHandler<GetSummaryByCountryQueryCommand, SummaryByCountryResult>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSummaryByCountryQueryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<SummaryByCountryResult> Handle(GetSummaryByCountryQueryCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Summary>();

            var query = repo.SingleResultQuery()
                .AndFilter(x => x.Slug == request.Country)
                .Select(x => new SummaryByCountryResult()
                {
                    Population = x.Population,
                    LastReceivedDate = x.LastReceivedDate,
                    TotalCases = x.TotalCases,
                    TotalDeaths = x.TotalDeaths,
                    TotalRecovered = x.TotalRecovered,
                    TotalCasesPerMillion = x.TotalCasesPerMillion,
                    TotalDeathsPerMillion = x.TotalDeathsPerMillion,
                    TotalRecoveredPerMillion = x.TotalRecoveredPerMillion
                });

            var result = await repo.FirstOrDefaultAsync(query)
                .ConfigureAwait(continueOnCapturedContext: false);

            return result;
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
