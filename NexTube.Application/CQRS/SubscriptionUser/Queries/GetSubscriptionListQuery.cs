
using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.SubscriptionUser.Queries
{
    public class GetSubscriptionListQuery : IRequest<GetSubscriptionsListQueryResult>
    {

        public int SubscriptionUserTo { get; set; }
    }
}