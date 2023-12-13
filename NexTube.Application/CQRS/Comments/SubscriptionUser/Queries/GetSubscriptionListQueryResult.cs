using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList
{
    public class GetSubscriptionsListQueryResult
    {
        public IList<SubscriptionLookup> Subscriptions { get; set; } = new List<SubscriptionLookup>();
    }
}