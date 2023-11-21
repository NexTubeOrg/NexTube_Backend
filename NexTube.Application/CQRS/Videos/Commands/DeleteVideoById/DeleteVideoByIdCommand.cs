using MediatR;

namespace NexTube.Application.CQRS.Videos.Commands.DeleteVideoById
{
    public class DeleteVideoByIdCommand : IRequest
    {
        public int VideoId { get; set; } = 0;
    }
}
