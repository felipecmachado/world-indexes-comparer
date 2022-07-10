using EntityFrameworkCore.UnitOfWork.Interfaces;
using WorldIndexesComparer.Application.Queries;
using WorldIndexesComparer.Application.Results;
using WorldIndexesComparer.Domain;
using WorldIndexesComparer.Domain.Coronavirus;
using WorldIndexesComparer.Domain.Countries;

namespace WorldIndexesComparer.Application.QueryHandlers
{
    public class GetCountriesComparisonQueryCommandHandler : IQueryHandler<GetCountriesComparisonQueryCommand, CountriesComparisonResult>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCountriesComparisonQueryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<CountriesComparisonResult> Handle(GetCountriesComparisonQueryCommand request, CancellationToken cancellationToken)
        {
            var result = new CountriesComparisonResult();

            var countryRepo = _unitOfWork.Repository<Country>();

            var query = countryRepo.MultipleResultQuery()
                .AndFilter(x => request.Countries.Contains(x.Slug));

            var countries = await countryRepo.SearchAsync(query)
                .ConfigureAwait(continueOnCapturedContext: false);

            foreach (var country in countries)
            {
                var summary = await GetSummaryAsync(country.Id)
                    .ConfigureAwait(continueOnCapturedContext: false);

                var stats = new CountryStatsResult()
                {
                    CountryName = country.Name,
                    Population = country.Population,
                    Year = 2022, // TODO: Hardcoded for now. In the future it will be able to choose the year
                    CoronavirusDeathsPerMillion = summary.TotalDeathsPerMillion,
                    ConsumerPriceIndex = 0 // TODO: Implement CPI 
                };

                result.Countries.Add(stats);
            }

            return result;
        }

        private async Task<Summary> GetSummaryAsync(Guid countryId)
        {
            var repo = _unitOfWork.Repository<Summary>();

            var query = repo.SingleResultQuery()
                .AndFilter(x => x.CountryId == countryId);

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
