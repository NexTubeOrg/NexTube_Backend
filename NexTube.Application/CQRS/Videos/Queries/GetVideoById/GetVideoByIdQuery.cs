using MediatR;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoById
{
    public class GetVideoByIdQuery : IRequest<GetVideoEntityQueryResult>
    {
        public int VideoId { get; set; } = 0;
    }
}
