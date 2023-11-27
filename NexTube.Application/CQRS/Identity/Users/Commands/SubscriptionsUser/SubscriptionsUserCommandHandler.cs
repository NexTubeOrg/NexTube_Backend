using MediatR;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;
using Ardalis.GuardClauses;
 
using System.ComponentModel.DataAnnotations;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser
{
    public class SubscriptionCommandHandler : IRequestHandler<Subscription, SubscriptionLookup>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationDbContext _dbContext;
        public SubscriptionCommandHandler(IDateTimeService dateTimeService, IApplicationDbContext dbContext)
        {
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
        }

        public async Task<SubscriptionLookup> Handle(Subscription request, CancellationToken cancellationToken)
        {
            // Перевірка даних
            var subscriber = await _dbContext.Users.FindAsync(request.SubscriberId);

            if (subscriber is null)
                throw new NotFoundException(request.SubscriberId.ToString(), nameof(UserEntity));

            var targetUser = await _dbContext.Users.FindAsync(request.UserId);

            if (targetUser is null)
                throw new NotFoundException(request.UserId.ToString(), nameof(UserEntity));

            if (subscriber.UserId == targetUser.UserId)
                throw new ValidationException("Subscriber cannot subscribe to themselves.");

            // Додавання підписки
            var subscription = new Subscription()
            {
                SubscriberId = (int)subscriber.UserId,
                UserId = (int)targetUser.UserId,
                DateCreated= _dateTimeService.Now,
            };

            _dbContext.Subscriptions.Add(subscription);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Повернення результату
            return new SubscriptionLookup()
            {
                SubscriptionId = subscription.Id,
                SubscriberId = subscription.SubscriberId,
                TargetUserId = subscription.UserId,
            };
        }
    }
}
