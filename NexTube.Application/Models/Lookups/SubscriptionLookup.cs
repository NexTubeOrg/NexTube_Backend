namespace NexTube.Application.Models.Lookups
{
    public class SubscriptionLookup
    {

        public UserLookup? Subscription { get; set; } = null;
        public DateTime? DateCreated { get; set; } = null;
    }
}