using MediatR;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoById
{
    public class GetVideoByIdQuery : IRequest<GetVideoByIdQueryResult>
    {
        public int VideoId { get; set; } = 0;
        public int? RequesterId { get; set; } = null;
    }
}
