using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NexTube.Domain.Entities.ManyToMany;

namespace NexTube.Persistence.Data.Configurations.Playlists {
    public class PlaylistsVideosManyToManyConfiguration : IEntityTypeConfiguration<PlaylistsVideosManyToMany> {
        public void Configure(EntityTypeBuilder<PlaylistsVideosManyToMany> builder) {
            builder.ToTable(nameof(PlaylistsVideosManyToMany));

            builder.HasKey(pv => new { pv.VideoId, pv.PlaylistId });

            builder.HasOne(pv => pv.Playlist)
                .WithMany(p => p.PlaylistsVideos)
                .HasForeignKey(pv => pv.PlaylistId);

            builder.HasOne(pv => pv.Video)
                .WithMany(v => v.PlaylistsVideos)
                .HasForeignKey(pv => pv.VideoId);
        }
    }
}
