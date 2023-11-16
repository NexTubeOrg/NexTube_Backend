using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities {
    public class VideoCommentEntity : OwnedEntity {
        public string Content { get; set; } = string.Empty;
        public VideoEntity VideoEntity { get; set; } = null!;
    }
}
