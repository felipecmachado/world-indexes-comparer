using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldIndexesComparer.Domain.Indexes;

namespace WorldIndexesComparer.Infrastructure.Data.Mappings
{
    public class IndexValueMap : IEntityTypeConfiguration<IndexValue>
    {
        public void Configure(EntityTypeBuilder<IndexValue> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.ToTable("IndexValues");
        }
    }
}