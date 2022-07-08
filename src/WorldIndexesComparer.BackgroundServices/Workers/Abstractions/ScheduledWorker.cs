using Cronos;
using System.Diagnostics;

namespace WorldIndexesComparer.BackgroundServices.Workers.Abstractions
{
    public abstract class ScheduledWorker : BackgroundService
    {
        protected CronExpression Expression { get; }
        protected TimeZoneInfo TimeZoneInfo { get; }
        protected ILogger Logger { get; }
        protected bool ShouldRunOnStartup { get; }

        protected string ServiceName => GetType().Name;

        protected ScheduledWorker(ILogger<ScheduledWorker> logger, string cronExpression, TimeZoneInfo timeZoneInfo, bool shouldRunOnStartup = false)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Expression = CronExpression.Parse(cronExpression) ?? throw new ArgumentException(nameof(cronExpression));
            TimeZoneInfo = timeZoneInfo ?? throw new ArgumentNullException(nameof(timeZoneInfo));
            ShouldRunOnStartup = shouldRunOnStartup;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (ShouldRunOnStartup)
            {
                Logger.LogInformation("{Job} was configured to run on the startup.", ServiceName);

                await InternalProcessAsync(stoppingToken).ConfigureAwait(continueOnCapturedContext: false);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                var nextRun = Expression.GetNextOccurrence(DateTimeOffset.UtcNow, TimeZoneInfo);
                var delay = nextRun.Value - DateTimeOffset.UtcNow;

                Logger.LogInformation("Next occurrence in {delay:c}.", delay);

                await Task.Delay(delay, stoppingToken).ConfigureAwait(continueOnCapturedContext: false);

                await InternalProcessAsync(stoppingToken).ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        private async Task InternalProcessAsync(CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            Exception unhandledException = null;

            var state = new Dictionary<string, object>
            {
                ["ProcessIdentifier"] = Guid.NewGuid()
            };

            IDisposable scope = Logger.BeginScope(state);

            try
            {
                Logger.LogInformation("{Job} is running right now.", ServiceName);

                await ProcessAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception ex)
            {
                unhandledException = ex;
            }
            finally
            {
                if (unhandledException != null)
                {
                    Logger.LogError(unhandledException, "{JobName} has ended unsuccessfully. Time taken: {Elapsed}s.", ServiceName, stopwatch.Elapsed.TotalSeconds);
                }
                else
                {
                    Logger.LogInformation("{JobName} has been successfully updated. Time taken: {Elapsed}s.", ServiceName, stopwatch.Elapsed.TotalSeconds);
                }

                scope?.Dispose();
            }
        }

        protected abstract Task ProcessAsync(CancellationToken stoppingToken);
    }
}
