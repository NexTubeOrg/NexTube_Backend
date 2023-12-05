using MediatR;
 


namespace NexTube.Application.CQRS.Identity.Users.Commands.GetUser
{
    public class GetUserCommand : IRequest<GetUserCommandResponse>
    {
        public int UserId { get; set; }
        
    }
}
     