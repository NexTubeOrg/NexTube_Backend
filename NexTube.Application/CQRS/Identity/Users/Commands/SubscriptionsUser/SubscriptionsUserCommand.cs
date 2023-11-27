
using MediatR;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser
{
    public class SubscriptionsUserCommand : IRequest<SubscriptionLookup>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubscriberId { get; set; }
        public DateTime DateCreated { get; set; }


    }
}