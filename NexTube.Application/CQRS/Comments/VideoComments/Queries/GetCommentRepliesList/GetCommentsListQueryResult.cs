using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentRepliesList {
    public class GetCommentRepliesListQueryResult {
        public IList<CommentLookup> Comments { get; set; } = new List<CommentLookup>();
    }
}
