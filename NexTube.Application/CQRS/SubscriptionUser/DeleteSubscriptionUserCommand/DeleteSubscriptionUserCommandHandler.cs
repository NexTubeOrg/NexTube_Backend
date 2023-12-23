// SubscriptionUserCommandHandler.cs
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.Identity.Users.Commands.AddSubscriptionUser;
using NexTube.Application.CQRS.Identity.Users.Commands.DeleteSubscriptionUserCommand;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.SubscriptionUser.DeleteSubscriptionUserCommand
{
    public class DeleteSubscriptionUserCommandHandler : IRequestHandler<DeleteSubscriptionUserCommand, DeleteSubscriptionUserCommandResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteSubscriptionUserCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<DeleteSubscriptionUserCommandResult> Handle(DeleteSubscriptionUserCommand request, CancellationToken cancellationToken)
        {
            var existingSubscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s =>
                    s.Creator.Id == request.User && s.Subscriber.Id == request.Subscriber,
                    cancellationToken);
            var info = await _userManager.FindByIdAsync(request.Subscriber.ToString());
            
            if (existingSubscription == null)
            {
                throw new NotFoundException(request.Subscriber.ToString(), nameof(SubscriptionEntity));

            }

            _context.Subscriptions.Remove(existingSubscription);

            await _context.SaveChangesAsync(cancellationToken);

             
            return new DeleteSubscriptionUserCommandResult()
            {
                UserId = info.Id,
                FirstName = info.FirstName,
                LastName = info.LastName,
                ChannelPhotoFileId = info.ChannelPhotoFileId.ToString()
            }; 
        }
    }
    }
