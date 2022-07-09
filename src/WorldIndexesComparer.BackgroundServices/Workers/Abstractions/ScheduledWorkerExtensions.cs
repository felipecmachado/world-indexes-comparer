namespace WorldIndexesComparer.BackgroundServices.Workers.Abstractions
{
    public static class ScheduledWorkerExtensions
    {
        public static IServiceCollection AddScheduledWorker<T>(this IServiceCollection services, Action<ScheduleConfig<T>> options)
            where T : ScheduledWorker
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }

            var config = new ScheduleConfig<T>();
            options.Invoke(config);

            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            services.AddSingleton(config);
            services.AddHostedService<T>();

            return services;
        }
    }
}
