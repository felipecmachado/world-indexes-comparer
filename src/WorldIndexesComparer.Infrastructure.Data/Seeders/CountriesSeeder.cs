using WorldIndexesComparer.Domain.Countries;

namespace WorldIndexesComparer.Infrastructure.Data.Seeders
{
    public static class CountriesSeeder
    {
        public static IList<Country> SeedCountries()
        {
            var countries = new List<Country>()
            {
                Country.New("Brazil", "BR", "BRA").SetSlug("brazil"),
                Country.New("Canada", "CA", "CAN").SetSlug("canada"),
                Country.New("United States of America", "US", "USA").SetSlug("united-states")
            };

            return countries;
        }
    }
}
