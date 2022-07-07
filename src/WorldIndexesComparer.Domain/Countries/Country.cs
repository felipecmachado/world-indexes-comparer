namespace WorldIndexesComparer.Domain.Countries
{
    public class Country
    {
        public Country() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string CountryCode { get; private set; }
        public int Population { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime ModifiedAt { get; private set; }

        public static Country New() => new Country()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        public Country SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            Name = name;

            return this;
        }

        public Country SetPopulation(int population)
        {
            if (population < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(population));
            }

            Population = population;

            return this;
        }

        public Country SetCountryCode(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode) || countryCode.Length > 3)
            {
                throw new ArgumentException(nameof(countryCode));
            }

            CountryCode = countryCode;

            return this;
        }

        public Country UpdatePopulation(int population)
        {
            if (population < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(population));
            }

            Population = population;
            ModifiedAt = DateTime.UtcNow;

            return this;
        }
    }
}