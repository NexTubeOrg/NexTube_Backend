
using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.SubscriptionUser.DeleteSubscriptionUserCommand
{
    public class DeleteSubscriptionUserCommand : IRequest<bool>
    {
        public int User { get; set; }
        public int Subscriber { get; set; }
    }
}