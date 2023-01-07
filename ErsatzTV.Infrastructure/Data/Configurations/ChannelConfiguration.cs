using ErsatzTV.Core.Domain;
using ErsatzTV.Core.Domain.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErsatzTV.Infrastructure.Data.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable("Channel");

        builder.HasIndex(c => c.Number)
            .IsUnique();

        builder.HasMany(c => c.Playouts) // TODO: is this correct, or should we have one to one?
            .WithOne(p => p.Channel)
            .HasForeignKey(p => p.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Artwork)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Watermark)
            .WithMany()
            .HasForeignKey(i => i.WatermarkId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasOne(i => i.FallbackFiller)
            .WithMany()
            .HasForeignKey(i => i.FallbackFillerId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.Property(c => c.Group)
            .IsRequired()
            .HasDefaultValue("ErsatzTV");

        builder.HasMany(c => c.ScheduleDayTemplates)
            .WithMany(dt => dt.Channels)
            .UsingEntity<ChannelScheduleDayTemplate>(
                j => j.HasOne(i => i.ScheduleDayTemplate)
                    .WithMany(i => i.ChannelScheduleDayTemplates)
                    .HasForeignKey(i => i.ScheduleDayTemplateId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(i => i.Channel)
                    .WithMany(i => i.ChannelScheduleDayTemplates)
                    .HasForeignKey(i => i.ChannelId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasKey(i => new { i.ChannelId, i.ScheduleDayTemplateId }));
    }
}
