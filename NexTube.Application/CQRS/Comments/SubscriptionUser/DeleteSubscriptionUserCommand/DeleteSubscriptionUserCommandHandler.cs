// SubscriptionUserCommandHandler.cs
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Subscriptions.Commands;
using NexTube.Domain.Entities;

namespace NexTube.Application.Subscriptions.Handlers
{
    public class DeleteSubscriptionUserCommandHandler : IRequestHandler<DeleteSubscriptionUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteSubscriptionUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<bool> Handle(DeleteSubscriptionUserCommand request, CancellationToken cancellationToken)
        {
            var existingSubscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s =>
                    s.Creator.Id == request.User && s.Subscriber.Id == request.Subscriber,
                    cancellationToken);

            if (existingSubscription == null)
            {
                // Підписки не існує
                return false;
            }

            _context.Subscriptions.Remove(existingSubscription);

            await _context.SaveChangesAsync(cancellationToken);

            // Повернути true, якщо підписка була успішно видалена
            return true;
        }
    }
}