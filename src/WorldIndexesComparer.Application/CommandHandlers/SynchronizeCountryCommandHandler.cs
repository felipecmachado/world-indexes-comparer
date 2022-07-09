using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using WorldIndexesComparer.Domain.Countries;
using WorldIndexesComparer.Domain.Countries.Commands;
using WorldIndexesComparer.Infrastructure.Extensions;

namespace WorldIndexesComparer.Application.Countries.CommandHandlers
{
    public class SynchronizeCountryCommandHandler : IRequestHandler<SynchronizeCountryCommand, bool>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public SynchronizeCountryCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(SynchronizeCountryCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Country>();
            var code = request.CCA2;

            var country = await GetExistingCountryAsync(code).ConfigureAwait(continueOnCapturedContext: false);

            if (country is null)
            {
                country = Country
                    .New(request.OfficialName, request.CCA2, request.CCA3)
                    .SetPopulation(request.Population);

                if (!string.IsNullOrEmpty(request.Slug))
                {
                    country.SetSlug(request.Slug);
                }

                repo.Add(country);
            }
            else
            {
                country.UpdatePopulation(request.Population);

                repo.Update(country);
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);

            await _mediator.DispatchDomainEventsAsync(country, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);

            return true;
        }

        private async Task<Country> GetExistingCountryAsync(string code)
        {
            var repo = _unitOfWork.Repository<Country>();

            var query = repo.SingleResultQuery()
                .AndFilter(x => x.CCA2 == code);

            var existingCountry = await repo.FirstOrDefaultAsync(query)
                .ConfigureAwait(continueOnCapturedContext: false);

            return existingCountry;
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
