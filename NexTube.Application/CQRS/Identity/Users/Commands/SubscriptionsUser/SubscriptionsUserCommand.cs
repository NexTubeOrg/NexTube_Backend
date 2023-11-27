
using MediatR;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser
{
    public class Subscription : IRequest<SubscriptionLookup>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubscriberId { get; set; }
        public DateTime DateCreated { get; set; }
        public Subscription()
        { }
            public Subscription(Subscription x)
        {
            this.Id = x.Id;
            this.UserId = x.UserId;
            this.SubscriberId = x.SubscriberId;
            this.DateCreated = x.DateCreated;
        }
    }
}