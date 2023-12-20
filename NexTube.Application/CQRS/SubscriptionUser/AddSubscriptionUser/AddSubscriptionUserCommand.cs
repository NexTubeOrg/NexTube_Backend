
using MediatR;
using NexTube.Application.CQRS.Identity.Users.Commands.AddSubscriptionUser;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.SubscriptionUser.AddSubscriptionUser
{
    public class AddSubscriptionUserCommand : IRequest<AddSubscriptionUserCommandResult>
    {
        public int User { get; set; }
        public int Subscriber { get; set; }
    }
}