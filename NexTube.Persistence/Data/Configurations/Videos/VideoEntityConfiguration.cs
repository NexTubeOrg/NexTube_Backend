using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexTube.Domain.Entities;

namespace NexTube.Persistence.Data.Configurations.Videos {
    public class VideoEntityConfiguration : IEntityTypeConfiguration<VideoEntity> {
        public void Configure(EntityTypeBuilder<VideoEntity> builder) {
            builder.HasKey(x => x.Id);

            // set null when user was deleted
            builder.HasOne(p => p.Creator)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(v => v.AccessModificator)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
