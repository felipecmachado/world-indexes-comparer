using WorldIndexesComparer.Domain.Countries.Events;

namespace WorldIndexesComparer.Domain.Countries
{
    public class Country : Entity, IAggregateRoot
    {
        public Country() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string CCA2 { get; private set; }
        public string CCA3 { get; private set; }
        public string Slug { get; private set; }
        public int Population { get; private set; }
        public string Continent { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }

        public static Country New(string name, string cca2, string cca3)
        {
            var country = new Country()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            }
            .SetName(name)
            .SetCountryCodes(cca2, cca3);

            country.AddDomainEvent(new CountrySynchronizedDomainEvent(country));

            return country;
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

        public Country SetSlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                throw new ArgumentException(nameof(slug));
            }

            Slug = slug;

            return this;
        }

        public Country SetContinent(string continent)
        {
            if (string.IsNullOrEmpty(continent))
            {
                throw new ArgumentException(nameof(continent));
            }

            Continent = continent;

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

        public void UpdatePopulation(int population)
        {
            if (population < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(population));
            }

            Population = population;
            ModifiedAt = DateTime.UtcNow;

            AddDomainEvent(new CountrySynchronizedDomainEvent(this));
        }
    }
}