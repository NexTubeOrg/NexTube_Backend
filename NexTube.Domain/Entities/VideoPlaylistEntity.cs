using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities {
    public class VideoPlaylistEntity : OwnedEntity {
        public string Title { get; set; } = null!;
        public virtual ICollection<VideoEntity> Videos { get; set; } = null!;
        public Guid? PreviewImage { get; set; }
    }
}
