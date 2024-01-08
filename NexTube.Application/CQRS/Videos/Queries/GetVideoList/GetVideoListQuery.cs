using MediatR;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoList
{
    public class GetVideoListQuery : IRequest<GetVideoListQueryResult>
    {
        public string? Name { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
