using MediatR;

namespace NexTube.Application.CQRS.Files.Videos.Commands.RemoveVideoByEntityId
{
    public class RemoveVideoByEntityIdCommand : IRequest
    {
        public int VideoEntityId { get; set; } = 0;
    }
}
