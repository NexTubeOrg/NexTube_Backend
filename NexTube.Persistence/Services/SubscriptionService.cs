using Ardalis.GuardClauses;
using MySql.Data.MySqlClient;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

public class SubscriptionService
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public SubscriptionService(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task SubscribeUserToUser(int subscriberId, int targetUserId)
    {
        // Додавання підписки
        await _subscriptionsRepository.AddSubscriptionAsync(subscriberId, targetUserId);
    }

    public async Task UnsubscribeUserFromUser(int subscriberId, int targetUserId)
    {
        // Видалення підписки
        await _subscriptionsRepository.DeleteSubscriptionAsync(subscriberId, targetUserId);
    }
    public async Task AddSubscriptionAsync(int subscriberId, int targetUserId)
    {
        // Check if the subscription exists
        var subscription = await _subscriptionsRepository.GetByIdAsync(subscriberId, targetUserId);
        if (subscription != null)
        {
            throw new ValidationException($"Subscription already exists for subscriber ID {subscriberId} and target user ID {targetUserId}");
        }

        // Create a new subscription
        var newSubscription = new SubscriptionsUserCommand
        {

            SubscriberId = subscriberId,
            TargetUserId = targetUserId,
        };

        // Save the subscription
        await _subscriptionsRepository.SaveAsync(newSubscription);
    }
    public async Task SaveAsync(SubscriptionsUserCommand subscription)
    {
        if (subscription == null)
        {
            throw new ArgumentNullException(nameof(subscription));
        }

        // Check if the subscription exists
        var existingSubscription = await _subscriptionsRepository.GetByIdAsync(subscription.SubscriberId, subscription.TargetUserId);
        if (existingSubscription != null)
        {
            // Update the existing subscription
            existingSubscription.SubscriberId = subscription.SubscriberId;
            existingSubscription.TargetUserId = subscription.TargetUserId;

            await _subscriptionsRepository.SaveAsync(existingSubscription);
        }
        else
        {
            // Create a new subscription
            var newSubscription = new SubscriptionsUserCommand
            {
                SubscriberId = subscription.SubscriberId,
                TargetUserId = subscription.TargetUserId,
            };

            await _subscriptionsRepository.SaveAsync(newSubscription);
        }
    }



    public async Task DeleteSubscriptionAsync(int subscriberId, int targetUserId)
    {
        // Check if the subscription exists
        var subscription = await _subscriptionsRepository.GetByIdAsync(subscriberId, targetUserId);
        if (subscription == null)
        {
            throw new NotFoundException("",$"Subscription does not exist for subscriber ID {subscriberId} and target user ID {targetUserId}");
        }

        // Delete the subscription
        await _subscriptionsRepository.DeleteAsync(subscription);
    }

}
