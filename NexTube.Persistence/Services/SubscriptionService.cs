using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;

public class SubscriptionService: ISubscriptionsRepository
{
    private readonly ISubscriptionsRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionsRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public IEnumerable<Subscription> GetSubscriptions(int userId)
    {
        return _subscriptionRepository.GetSubscriptions(userId);
    }

    public void Subscribe(Subscription subscriber )
    {
        var subscription = new Subscription
        {
            UserId = subscriber.UserId,
            SubscriberId = subscriber.SubscriberId,
            DateCreated = subscriber.DateCreated
        };

        _subscriptionRepository.Subscribe(subscription);
    }

    public void Unsubscribe(Subscription subscriber)
    {
        _subscriptionRepository.Unsubscribe(new Subscription
        {
            UserId = subscriber.UserId,
            SubscriberId = subscriber.SubscriberId,
        });
    }
}
