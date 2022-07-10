using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldIndexesComparer.Domain.Countries;

namespace WorldIndexesComparer.Infrastructure.Data.Mappings
{
    using Index = Domain.Indexes.Index;

    public class IndexMap : IEntityTypeConfiguration<Index>
    {
        public void Configure(EntityTypeBuilder<Index> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Ticker)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Description)
                .HasMaxLength(256);

            builder.Property(x => x.Periodicity)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired();

            builder.HasOne<Country>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(i => i.CountryId);

            builder.ToTable("Indexes");
        }
    }
}