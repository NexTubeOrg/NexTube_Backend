using MediatR;



namespace NexTube.Application.CQRS.Identity.Users.Commands.GetUser
{
    public class GetChannelInfoCommand : IRequest<GetChannelInfoCommandResult>
    {
        public int UserId { get; set; }

    }
}
