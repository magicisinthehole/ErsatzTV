using ErsatzTV.Core.Domain.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErsatzTV.Infrastructure.Data.Configurations.Scheduling;

public class ScheduleBlockConfiguration : IEntityTypeConfiguration<ScheduleBlock>
{
    public void Configure(EntityTypeBuilder<ScheduleBlock> builder)
    {
        builder.ToTable("ScheduleBlock");

        builder.HasIndex(sb => sb.Name).IsUnique();

        builder.HasMany(sb => sb.Items)
            .WithOne(i => i.ScheduleBlock)
            .HasForeignKey(i => i.ScheduleBlockId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
