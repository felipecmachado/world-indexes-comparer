namespace WorldIndexesComparer.Domain.Countries
{
    public class Country
    {
        public Country() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string CCA2 { get; private set; }
        public string CCA3 { get; private set; }
        public int Population { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }

        public static Country New(string name, string cca2, string cca3)
        {
            return new Country()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            }
            .SetName(name)
            .SetCountryCodes(cca2, cca3);
        }

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

        public Country SetCountryCodes(string cca2, string cca3)
        {
            if (string.IsNullOrEmpty(cca2) || cca2.Length > 2)
            {
                throw new ArgumentException(nameof(cca2));
            }

            if (string.IsNullOrEmpty(cca3) || cca3.Length > 3)
            {
                throw new ArgumentException(nameof(cca3));
            }

            CCA2 = cca2;
            CCA3 = cca3;

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