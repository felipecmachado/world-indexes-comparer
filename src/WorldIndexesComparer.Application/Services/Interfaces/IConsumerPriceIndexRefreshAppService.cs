namespace WorldIndexesComparer.Application.Services.Interfaces
{
    public interface IConsumerPriceIndexRefreshAppService
    {
        Task SyncConsumerPriceIndexes(CancellationToken stoppingToken);
    }
}
