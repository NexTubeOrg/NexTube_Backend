 
using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.Subscriptions.Commands
{
    public class AddSubscriptionUserCommand : IRequest<bool>
    {
        public int User { get; set; }
        public int Subscriber { get; set; }
    }
}
