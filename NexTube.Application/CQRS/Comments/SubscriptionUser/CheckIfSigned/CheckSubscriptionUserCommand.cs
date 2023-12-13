
using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.Subscriptions.Commands
{
    public class CheckSubscriptionUserCommand : IRequest<bool>
    {
        public int UserID { get; set; }
        public int SubscriptionUserTo { get; set; }
    }
}