using MediatR;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideo
{
    public class GetVideoQuery : IRequest<GetVideoQueryResult>
    {
        public int VideoId { get; set; } = 0;
        public int? RequesterId { get; set; } = null;
    }
}
