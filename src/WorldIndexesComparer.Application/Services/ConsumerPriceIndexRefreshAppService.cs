using Covid19.Client;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using WorldIndexesComparer.Application.Services.Interfaces;
using WorldIndexesComparer.Domain.Coronavirus;
using WorldIndexesComparer.Domain.Coronavirus.Commands;

namespace WorldIndexesComparer.Application.Services
{
    public class ConsumerPriceIndexRefreshAppService : IConsumerPriceIndexRefreshAppService
    {
        private readonly ILogger<ConsumerPriceIndexRefreshAppService> _logger;
        private readonly ICoronavirusClient _coronavirusClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ConsumerPriceIndexRefreshAppService(
            ILogger<ConsumerPriceIndexRefreshAppService> logger,
            ICoronavirusClient coronavirusClient,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _coronavirusClient = coronavirusClient ?? throw new ArgumentNullException(nameof(coronavirusClient));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task SyncConsumerPriceIndexes(CancellationToken stoppingToken)
        {
            var summaries = await GetSummariesToUpdateAsync().ConfigureAwait(continueOnCapturedContext: false);

            foreach (var summary in summaries)
            {
                stoppingToken.ThrowIfCancellationRequested();

                var records = await _coronavirusClient.GetTotalsByCountry(summary.Slug).ConfigureAwait(continueOnCapturedContext: false);

                // TODO: Save all the history instead of the last record

                var lastRecord = records?.OrderByDescending(x => x.Date).FirstOrDefault();

                if (lastRecord != null)
                {
                    var command = new UpdateCoronavirusSummaryCommand()
                    {
                        Summary = summary,
                        Active = lastRecord.Active,
                        Confirmed = lastRecord.Confirmed,
                        Deaths = lastRecord.Deaths,
                        Recovered = lastRecord.Recovered,
                        Date = lastRecord.Date
                    };

                    var response = await _mediator.Send(command).ConfigureAwait(continueOnCapturedContext: false);
                }
            }
        }

        private async Task<IList<Summary>> GetSummariesToUpdateAsync()
        {
            var repo = _unitOfWork.Repository<Summary>();

            Expression<Func<Summary, bool>> isOutdated = (summary) => 
                summary.LastUpdatedAt == null || summary.LastUpdatedAt < DateTime.UtcNow.AddHours(-8);
            
            var query = repo.MultipleResultQuery()
                .Top(3)
                .AndFilter(isOutdated)
                .OrderBy(x => x.LastReceivedDate)
                .ThenByDescending(x => x.Population);

            var summaries = await repo.SearchAsync(query)
                .ConfigureAwait(continueOnCapturedContext: false);

            return summaries;
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
