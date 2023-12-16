using MediatR;

namespace NexTube.Application.CQRS.Videos.Commands.DeleteVideoAsModerator
{
    public class DeleteVideoAsModeratorCommand : IRequest
    {
        public int? VideoId { get; set; } 
    }
}
