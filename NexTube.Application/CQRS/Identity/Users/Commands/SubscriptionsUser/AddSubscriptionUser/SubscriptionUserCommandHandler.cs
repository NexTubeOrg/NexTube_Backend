// SubscriptionUserCommandHandler.cs
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.Identity.Users.Commands.CreateUser;
using NexTube.Application.Subscriptions.Commands;
using NexTube.Domain.Entities;

namespace NexTube.Application.Subscriptions.Handlers
{
    public class SubscriptionUserCommandHandler : IRequestHandler<SubscriptionUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SubscriptionUserCommandHandler(UserManager<ApplicationUser> userManager,IApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<bool> Handle(SubscriptionUserCommand request, CancellationToken cancellationToken)
        {
            var existingSubscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s =>
                    s.User.Id == request.User && s.Subscriber.Id == request.Subscriber,
                    cancellationToken);

            if (existingSubscription != null)
            {
                // Користувач вже підписаний
                return false;
            }

            var subscriptionEntity = new SubscriptionEntity
            {
                User = await _userManager.FindByIdAsync(request.User.ToString()),
                Subscriber = await _userManager.FindByIdAsync(request.User.ToString()),
            };

            _context.Subscriptions.Add(subscriptionEntity);

            await _context.SaveChangesAsync(cancellationToken);

            // Повернути true, якщо підписка вдалася
            return true;
        }
    }
}
