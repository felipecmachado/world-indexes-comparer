using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldIndexesComparer.Domain.Countries;

namespace WorldIndexesComparer.Infrastructure.Data.Mappings
{
    public class CountryMap : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.CCA2)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(x => x.CCA3)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.Population)
                .IsRequired();

            builder.ToTable("Countries");
        }
    }
}