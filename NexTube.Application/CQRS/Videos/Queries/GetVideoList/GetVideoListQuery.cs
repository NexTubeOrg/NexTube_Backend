using MediatR;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoList
{
    public class GetVideoListQuery : IRequest<GetVideoListQueryResult>
    {
        public int? RequesterId { get; set; } = null;
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
