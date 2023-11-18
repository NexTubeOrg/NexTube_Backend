using MediatR;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity
{
    public class GetVideoEntityQuery : IRequest<GetVideoEntityQueryResult>
    {
        public int VideoEntityId { get; set; } = 0;
    }
}
