using MediatR;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoListChannel
{
    public class GetVideoListChannelQuery : IRequest<GetVideoListChannelQueryResult>
    {
        public int? RequesterId { get; set; } = null;
        public int ChannelId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
