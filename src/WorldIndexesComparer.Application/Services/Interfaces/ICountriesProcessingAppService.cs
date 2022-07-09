namespace WorldIndexesComparer.Application.Services.Interfaces
{
    public interface ICountriesProcessingAppService : IDisposable
    {
        Task SyncAllCountriesAsync(CancellationToken stoppingToken);
    }
}
