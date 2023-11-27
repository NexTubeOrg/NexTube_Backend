using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities
{
    public class SubscriptionEntity : OwnedEntity
    {
        public ApplicationUser User { get; set; }
        public ApplicationUser Subscriber { get; set; }
    
    }
}
