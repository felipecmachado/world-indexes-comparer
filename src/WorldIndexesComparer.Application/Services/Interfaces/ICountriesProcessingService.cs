namespace WorldIndexesComparer.Application.Services.Interfaces
{
    public interface ICountriesProcessingService : IDisposable
    {
        Task SyncAllCountriesAsync(CancellationToken stoppingToken);
    }
}
