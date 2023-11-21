using MediatR;

namespace NexTube.Application.CQRS.Videos.Queries.GetAllVideos
{
    public class GetAllVideosQuery : IRequest<GetAllVideosQueryResult>
    {
    }
}
