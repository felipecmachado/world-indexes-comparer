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
            builder.Property(x => x.LastReceivedDate);

            builder.Ignore(x => x.TotalCasesPerMillion);

            builder.Ignore(x => x.TotalDeathsPerMillion);

            builder.Ignore(x => x.TotalRecoveredPerMillion);

            builder.HasOne<Country>()
                .WithOne()
                .IsRequired(false);

            builder.HasIndex(x => x.CountryId)
                 .IsUnique();

            builder.ToTable("CoronavirusSummaries");
        }
    }
}