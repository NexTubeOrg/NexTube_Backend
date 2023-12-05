namespace NexTube.Application.Models.Lookups {
    public class CommentLookup {
        public int? CommentId { get; set; } = null;
        public string Content { get; set; } = string.Empty;
        public UserLookup? Creator { get; set; } = null;
        public DateTime? DateCreated { get; set; } = null;
        public int RepliesCount { get; set; } = 0;
    }
}
