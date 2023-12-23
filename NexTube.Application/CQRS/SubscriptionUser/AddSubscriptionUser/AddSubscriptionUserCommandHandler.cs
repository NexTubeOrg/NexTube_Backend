// SubscriptionUserCommandHandler.cs
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.Identity.Users.Commands.AddSubscriptionUser;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.SubscriptionUser.AddSubscriptionUser
{
    public class AddSubscriptionUserCommandHandler : IRequestHandler<AddSubscriptionUserCommand, AddSubscriptionUserCommandResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AddSubscriptionUserCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<AddSubscriptionUserCommandResult> Handle(AddSubscriptionUserCommand request, CancellationToken cancellationToken)
        {
            if (request.User == request.Subscriber)
            {
                throw new NotFoundException(request.Subscriber.ToString(), nameof(SubscriptionEntity));
            }
            var existingSubscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s =>
                    s.Creator.Id == request.User && s.Subscriber.Id == request.Subscriber ,
                    cancellationToken);

            if (existingSubscription != null)
            {
                
                throw new NotFoundException(request.Subscriber.ToString(), nameof(SubscriptionEntity));

            }

            var subscriptionEntity = new SubscriptionEntity
            {
                Creator = await _userManager.FindByIdAsync(request.User.ToString()),
                Subscriber = await _userManager.FindByIdAsync(request.Subscriber.ToString()),
            };

            _context.Subscriptions.Add(subscriptionEntity);

            await _context.SaveChangesAsync(cancellationToken);

            // Повернути true, якщо підписка вдалася
            return new AddSubscriptionUserCommandResult() { 
                UserId = subscriptionEntity.Subscriber.Id, 
                FirstName = subscriptionEntity.Subscriber.FirstName, 
                LastName = subscriptionEntity.Subscriber.LastName,
                ChannelPhotoFileId = subscriptionEntity.Subscriber.ChannelPhotoFileId.ToString() };
        }
    }
}