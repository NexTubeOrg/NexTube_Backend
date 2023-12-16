using MediatR;

namespace NexTube.Application.CQRS.Identity.Users.Commands.AssignModerator
{
    public class AssignModeratorCommand : IRequest<AssignModeratorCommandResult>
    {
        public int UserId { get; set; } 
    }
}
