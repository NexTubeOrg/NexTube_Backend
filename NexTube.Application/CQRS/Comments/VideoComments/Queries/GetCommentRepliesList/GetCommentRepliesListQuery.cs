using MediatR;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentRepliesList {
    public class GetCommentRepliesListQuery : IRequest<GetCommentRepliesListQueryResult> {
        public int? VideoId { get; set; } = null!;
        public int? RootCommentId { get; set; } = null!;
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
