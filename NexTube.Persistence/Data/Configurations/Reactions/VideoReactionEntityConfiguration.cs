using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NexTube.Domain.Entities;

namespace NexTube.Persistence.Data.Configurations.Reactions {
    public class VideoReactionEntityConfiguration : IEntityTypeConfiguration<VideoReactionEntity> {
        public void Configure(EntityTypeBuilder<VideoReactionEntity> builder) {
            builder.HasKey(x => new {
                x.CreatorId,
                x.ReactedVideoId
            });

            builder.HasOne(r => r.Creator)
                .WithMany()
                .HasForeignKey(x => x.CreatorId);

            builder.HasOne(r => r.ReactedVideo)
                .WithMany()
                .HasForeignKey(x => x.ReactedVideoId);

            builder.Property(r => r.Type)
                .IsRequired();
        }
    }
}
