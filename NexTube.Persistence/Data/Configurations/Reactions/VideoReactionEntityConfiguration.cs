using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NexTube.Domain.Entities;

namespace NexTube.Persistence.Data.Configurations.Reactions {
    public class VideoReactionEntityConfiguration : IEntityTypeConfiguration<VideoReactionEntity> {
        public void Configure(EntityTypeBuilder<VideoReactionEntity> builder) {
            builder.HasKey(x => x.Id);

            // remove like when user was deleted
            builder.HasOne(r => r.Creator)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            // remove like when video was deleted
            builder.HasOne(r => r.ReactedVideo)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(r => r.Type)
                .IsRequired();
        }
    }
}
