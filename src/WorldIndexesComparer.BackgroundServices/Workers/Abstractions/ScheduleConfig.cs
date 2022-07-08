namespace WorldIndexesComparer.BackgroundServices.Workers.Abstractions
{
    public class ScheduleConfig<T>
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
        public bool ShouldRunOnStartup { get; set; }
    }
}
