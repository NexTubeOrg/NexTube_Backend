using MediatR;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList
{
    public class GetCommentsListQuery : IRequest<GetCommentsListQueryResult>
    {
        public int? VideoId { get; set; } = null!;
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
