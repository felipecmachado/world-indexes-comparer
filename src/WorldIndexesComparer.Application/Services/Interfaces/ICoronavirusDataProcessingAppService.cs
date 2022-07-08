namespace WorldIndexesComparer.Application.Services.Interfaces
{
    public interface ICoronavirusDataProcessingAppService : IDisposable
    {
        Task SyncAllCountriesAsync(CancellationToken stoppingToken);
    }
}
