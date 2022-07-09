namespace WorldIndexesComparer.Application.Countries.Services.Interfaces
{
    public interface ICountriesProcessingAppService : IDisposable
    {
        Task SyncAllCountriesAsync(CancellationToken stoppingToken);
    }
}
