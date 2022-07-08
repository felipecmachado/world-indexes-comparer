using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldIndexesComparer.Domain.Coronavirus;

namespace WorldIndexesComparer.Infrastructure.Data.Mappings
{
    public class DailyStatsMap : IEntityTypeConfiguration<DailyStats>
    {
        public void Configure(EntityTypeBuilder<DailyStats> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Cases)
                .IsRequired();
            builder.Property(x => x.Deaths)
                .IsRequired();
            builder.Property(x => x.Recovered)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.ToTable("CoronavirusDailyStats");
        }
    }
}