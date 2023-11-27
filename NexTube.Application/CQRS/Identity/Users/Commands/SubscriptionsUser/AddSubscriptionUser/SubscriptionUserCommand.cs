// SubscriptionUserCommand.cs
using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.Subscriptions.Commands
{
    public class SubscriptionUserCommand : IRequest<bool>
    {
        public int User { get; set; }
        public int Subscriber { get; set; }
    }
}
