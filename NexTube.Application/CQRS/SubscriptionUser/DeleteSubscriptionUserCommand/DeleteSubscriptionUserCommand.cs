
using MediatR;
using NexTube.Application.CQRS.Identity.Users.Commands.AddSubscriptionUser;
using NexTube.Application.CQRS.Identity.Users.Commands.DeleteSubscriptionUserCommand;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.SubscriptionUser.DeleteSubscriptionUserCommand
{
    public class DeleteSubscriptionUserCommand : IRequest<DeleteSubscriptionUserCommandResult>
    {
        public int User { get; set; }
        public int Subscriber { get; set; }
    }
}