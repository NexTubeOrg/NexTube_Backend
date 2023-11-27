namespace NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser
{
    public  class SubscriptionLookup
    {
        public SubscriptionLookup()
        {
        }

        public int SubscriptionId { get; set; }
        public object SubscriberId { get; set; }
        public object TargetUserId { get; set; }
    }
}