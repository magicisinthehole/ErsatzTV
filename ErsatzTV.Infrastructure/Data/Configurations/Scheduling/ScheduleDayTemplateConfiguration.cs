using ErsatzTV.Core.Domain.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErsatzTV.Infrastructure.Data.Configurations.Scheduling;

public class ScheduleDayTemplateConfiguration : IEntityTypeConfiguration<ScheduleDayTemplate>
{
    public void Configure(EntityTypeBuilder<ScheduleDayTemplate> builder)
    {
        builder.ToTable("ScheduleDayTemplate");

        builder.HasMany(t => t.ScheduleBlocks)
            .WithMany(b => b.DayTemplates)
            .UsingEntity<ScheduleDayTemplateItem>(
                j => j.HasOne(i => i.ScheduleBlock)
                    .WithMany(i => i.DayTemplateItems)
                    .HasForeignKey(i => i.ScheduleBlockId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(i => i.ScheduleDayTemplate)
                    .WithMany(i => i.Items)
                    .HasForeignKey(i => i.ScheduleDayTemplateId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasKey(i => new { i.ScheduleDayTemplateId, i.ScheduleBlockId }));
    }
}
