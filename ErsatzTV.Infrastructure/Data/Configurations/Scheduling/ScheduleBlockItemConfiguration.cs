using ErsatzTV.Core.Domain.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErsatzTV.Infrastructure.Data.Configurations.Scheduling;

public class ScheduleBlockItemConfiguration : IEntityTypeConfiguration<ScheduleBlockItem>
{
    public void Configure(EntityTypeBuilder<ScheduleBlockItem> builder)
    {
        builder.ToTable("ScheduleBlockItem");
    }
}
