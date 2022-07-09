namespace WorldIndexesComparer.Application.Services.Interfaces
{
    public interface ICoronavirusDataProcessingAppService : IDisposable
    {
        Task RefreshDataAsync(CancellationToken stoppingToken);
    }
}
