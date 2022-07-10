using WorldIndexesComparer.BackgroundServices.Workers.Abstractions;

namespace WorldIndexesComparer.BackgroundServices.Workers
{
    public class ConsumerPriceIndexDataCollectionWorker : ScheduledWorker
    {
        private readonly ILogger<ConsumerPriceIndexDataCollectionWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public ConsumerPriceIndexDataCollectionWorker(
            ILogger<ConsumerPriceIndexDataCollectionWorker> logger,
            ScheduleConfig<ConsumerPriceIndexDataCollectionWorker> config,
            IServiceScopeFactory scopeFactory)
            : base(logger, config.CronExpression, config.TimeZoneInfo, config.ShouldRunOnStartup)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ProcessAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                //var service = scope.ServiceProvider.GetRequiredService<I>();

                // await service.RefreshDataAsync(stoppingToken).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the countries.");
            }
        }

    }
}