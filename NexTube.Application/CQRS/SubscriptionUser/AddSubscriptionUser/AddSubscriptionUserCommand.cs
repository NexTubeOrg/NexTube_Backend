
using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.SubscriptionUser.AddSubscriptionUser
{
    public class AddSubscriptionUserCommand : IRequest<bool>
    {
        public int User { get; set; }
        public int Subscriber { get; set; }
    }
}