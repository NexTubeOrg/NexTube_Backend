using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities {
    public class VideoReactionEntity : OwnedEntity {
        public enum VideoReactionType {
            Like, Dislike
        }
        public VideoEntity ReactedVideo { get; set; } = null!;
        public VideoReactionType Type { get; set; }
    }
}
