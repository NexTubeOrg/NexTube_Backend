using NexTube.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexTube.Domain.Entities {
    public class VideoReactionEntity : IEntity, ITimeModification {
        public enum VideoReactionType {
            Like, Dislike
        }

        public int? ReactedVideoId { get; set; }
        public VideoEntity ReactedVideo { get; set; } = null!;

        public int? CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; } = null!;

        public VideoReactionType Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
