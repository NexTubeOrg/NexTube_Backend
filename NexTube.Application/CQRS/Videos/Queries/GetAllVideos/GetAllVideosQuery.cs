using MediatR;

namespace NexTube.Application.CQRS.Videos.Queries.GetAllVideos
{
    public class GetAllVideosQuery : IRequest<GetAllVideosQueryResult>
    {
        public int? RequesterId { get; set; } = null;
    }
}
