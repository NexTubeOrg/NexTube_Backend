using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList {
    public class GetCommentsListQueryResult {
        public IList<CommentLookup> Comments { get; set; } = new List<CommentLookup>();
        public int TotalCount { get; set; }
    }
}
