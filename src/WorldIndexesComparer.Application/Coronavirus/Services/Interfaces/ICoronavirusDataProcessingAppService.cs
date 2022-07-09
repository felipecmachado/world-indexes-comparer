namespace WorldIndexesComparer.Application.Coronavirus.Services.Interfaces
{
    public interface ICoronavirusDataProcessingAppService : IDisposable
    {
        Task RefreshDataAsync(CancellationToken stoppingToken);
    }
}
