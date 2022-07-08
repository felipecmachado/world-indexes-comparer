using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using WorldIndexesComparer.Domain.Countries;
using WorldIndexesComparer.Infrastructure.Data.Contexts;

namespace WorldIndexesComparer.Infrastructure.Data.Seeders
{
    public static class WorldDataInitializer
    {
        public static void WorldDataContextInitializeAndSeed(this IServiceCollection services)
        {
            var serviceScopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

            using var serviceScope = serviceScopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<WorldDataContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
 
            // Initial data
            SeedCountries(serviceScope);
        }

        private static void SeedCountries(IServiceScope serviceScope)
        {
            var unitOfWork = serviceScope.ServiceProvider.GetService<IUnitOfWork>();
            var repository = unitOfWork.Repository<Country>();

            if (!repository.Any())
            {
                var countries = CountriesSeeder.SeedCountries();

                repository.AddRange(countries);
                unitOfWork.SaveChanges();
            }
        }
    }
}
