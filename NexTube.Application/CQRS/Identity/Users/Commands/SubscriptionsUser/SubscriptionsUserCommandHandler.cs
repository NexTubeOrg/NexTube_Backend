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
using Microsoft.AspNetCore.Identity;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser
{
    public class SubscriptionCommandHandler : IRequestHandler<SubscriptionsUserCommand, SubscriptionLookup>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public SubscriptionCommandHandler(UserManager<ApplicationUser> userManager, IDateTimeService dateTimeService, IApplicationDbContext dbContext)
        {
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
            _userManager = userManager;

        }

        public async Task<SubscriptionLookup> Handle(SubscriptionsUserCommand request, CancellationToken cancellationToken)
        {
            // Перевірка даних
            
            var subscriber = await _userManager.FindByIdAsync(request.SubscriberId.ToString());
          
            if (subscriber is null)
                throw new NotFoundException(request.SubscriberId.ToString(), nameof(ApplicationUser));

            var targetUser = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (targetUser is null)
                throw new NotFoundException(request.UserId.ToString(), nameof(ApplicationUser));

            if (subscriber.Id == targetUser.Id)
                throw new ValidationException("Subscriber cannot subscribe to themselves.");

            // Додавання підписки
            var subscription = new SubscriptionEntity()
            {
                SubscriberId = (int)subscriber.Id,
                UserId = (int)targetUser.Id,
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
