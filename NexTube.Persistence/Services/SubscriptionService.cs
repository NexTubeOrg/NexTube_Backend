using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using NexTube.Domain.Entities;

public class SubscriptionService: ISubscriptionsService
{
    private readonly ISubscriptionsService _subscriptionRepository;

    public SubscriptionService(ISubscriptionsService subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public IEnumerable<SubscriptionEntity> GetSubscriptions(int userId)
    {
        return _subscriptionRepository.GetSubscriptions(userId);
    }

    public Task<Result> Subscribe(SubscriptionEntity subscriber )
    {
        var subscription = new SubscriptionEntity
        {
            UserId = subscriber.UserId,
            SubscriberId = subscriber.SubscriberId,
      
        };

        return _subscriptionRepository.Subscribe(subscription);
    }

    public Task<Result> Unsubscribe(SubscriptionEntity subscriber)
    {
      return   _subscriptionRepository.Unsubscribe(new SubscriptionEntity
      {
            UserId = subscriber.UserId,
            SubscriberId = subscriber.SubscriberId,
        });
    }
}
