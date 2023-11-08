using MediatR;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity
{
    public class GetVideoEntityQuery : IRequest<GetVideoEntityQueryVm>
    {
        public int VideoEntityId { get; set; } = 0;
    }
}
