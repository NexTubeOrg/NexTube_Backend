using MediatR;

namespace NexTube.Application.CQRS.Videos.Commands.DeleteVideo
{
    public class DeleteVideoCommand : IRequest
    {
        public int VideoId { get; set; } = 0;
        public int? RequsterId { get; set; } = null!;
    }
}
