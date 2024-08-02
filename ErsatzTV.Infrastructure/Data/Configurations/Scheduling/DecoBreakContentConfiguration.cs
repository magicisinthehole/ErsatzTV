using ErsatzTV.Core.Domain.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErsatzTV.Infrastructure.Data.Configurations.Scheduling;

public class DecoBreakContentConfiguration : IEntityTypeConfiguration<DecoBreakContent>
{
    public void Configure(EntityTypeBuilder<DecoBreakContent> builder)
    {
        builder.ToTable("DecoBreakContent");

        builder.HasOne(d => d.Collection)
            .WithMany()
            .HasForeignKey(d => d.CollectionId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasOne(d => d.MediaItem)
            .WithMany()
            .HasForeignKey(d => d.MediaItemId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasOne(d => d.MultiCollection)
            .WithMany()
            .HasForeignKey(d => d.MultiCollectionId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasOne(d => d.SmartCollection)
            .WithMany()
            .HasForeignKey(d => d.SmartCollectionId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}
