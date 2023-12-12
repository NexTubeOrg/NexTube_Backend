using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities
{
    public class SubscriptionEntity : OwnedEntity
    {
         
        public ApplicationUser Subscriber { get; set; }

    }
}