using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;

namespace NexTube.Application.CQRS.Videos.Queries.GetCommentsList {
    public class CommentLookup {
        public string Content { get; set; } = string.Empty;
        public UserLookup? Creator { get; set; } = null;
        public DateTime? DateCreated { get; set; } = null;
    }
}
