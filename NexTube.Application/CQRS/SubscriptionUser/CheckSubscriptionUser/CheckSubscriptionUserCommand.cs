
using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.SubscriptionUser.CheckSubscriptionUser
{
    public class CheckSubscriptionUserCommand : IRequest<bool>
    {
        public int UserID { get; set; }
        public int SubscriptionUserTo { get; set; }
    }
}