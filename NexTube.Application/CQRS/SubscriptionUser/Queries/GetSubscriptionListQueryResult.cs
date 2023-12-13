using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.SubscriptionUser.Queries
{
    public class GetSubscriptionsListQueryResult
    {
        public IList<SubscriptionLookup> Subscriptions { get; set; } = new List<SubscriptionLookup>();
    }
}