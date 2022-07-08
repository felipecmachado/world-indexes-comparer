using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldIndexesComparer.Domain.Coronavirus;
using WorldIndexesComparer.Domain.Countries;

namespace WorldIndexesComparer.Infrastructure.Data.Mappings
{
    public class SummaryMap : IEntityTypeConfiguration<Summary>
    {
        public void Configure(EntityTypeBuilder<Summary> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.LastReceivedDate)
                .IsRequired();

            builder.HasOne<Country>()
                .WithOne()
                .IsRequired(false);

            builder.ToTable("CoronavirusSummaries");
        }
    }
}