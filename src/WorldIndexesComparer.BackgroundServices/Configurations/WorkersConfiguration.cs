using WorldIndexesComparer.BackgroundServices.Workers;
using WorldIndexesComparer.BackgroundServices.Workers.Abstractions;

namespace WorldIndexesComparer.BackgroundServices.Configurations
{
    public static class WorkersConfiguration
    {
        public static IServiceCollection AddWorkersConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            /// For more information about the cron expressions
            /// you can visit the following repo: https://github.com/HangfireIO/Cronos

            services.AddScheduledJob<CountriesDataCollectionWorker>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.ShouldRunOnStartup = true;
                c.CronExpression = @"0 6 * * *"; // every day at 6AM
            });

            //services.AddScheduledJob<CoronavirusDataCollectionWorker>(c =>
            //{
            //    c.TimeZoneInfo = TimeZoneInfo.Local;
            //    c.ShouldRunOnStartup = true;
            //    c.CronExpression = @"*/30 * * * *"; // every 30 minutes
            //});
            
            //services.AddScheduledJob<ConsumerPriceIndexDataCollectionWorker>(c =>
            //{
            //    c.TimeZoneInfo = TimeZoneInfo.Local;
            //    c.ShouldRunOnStartup = true;
            //    c.CronExpression = @"*/30 * * * *"; // every 30 minutes
            //});

            return services;
        }
    }
}