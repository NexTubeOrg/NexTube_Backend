using NexTube.Domain.Entities.Abstract;
using NexTube.Domain.Entities.ManyToMany;

namespace NexTube.Domain.Entities {
    public class VideoPlaylistEntity : OwnedEntity {
        public string Title { get; set; } = null!;
        public Guid? PreviewImage { get; set; }

        public virtual ICollection<PlaylistsVideosManyToMany> PlaylistsVideos { get; set; } = null!;
    }
}
