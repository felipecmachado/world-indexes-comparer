using WorldIndexesComparer.Domain.Countries;

namespace WorldIndexesComparer.Infrastructure.Data.Seeders
{
    public static class CountriesSeeder
    {
        public static IList<Country> SeedCountries()
        {
            var countries = new List<Country>()
            {
                Country.New("Brazil", "BR", "BRA"),
                Country.New("Canada", "CA", "CAN"),
                Country.New("United States", "US", "USA")
            };

            return countries;
        }
    }
}
