using MediatR;

namespace WorldIndexesComparer.Domain.Countries.Events
{
    public class CountrySynchronizedDomainEvent : INotification
    {
        public CountrySynchronizedDomainEvent(Country country)
        {
            Country = country;
        }

        public Country Country { get; }
    }
}
