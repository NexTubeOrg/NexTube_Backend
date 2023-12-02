﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList;
using NexTube.Application.Models.Lookups;
using NexTube.Application.Subscriptions.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class CheckSubscriptionUserCommandHandler : IRequestHandler<CheckSubscriptionUserCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CheckSubscriptionUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CheckSubscriptionUserCommand request, CancellationToken cancellationToken)
    {

        bool isSubscribed = await _context.Subscriptions
     .AnyAsync(s =>s.User.Id == request.UserID && s.Subscriber.Id == request.SubscriptionUserTo);

        return isSubscribed;
    }
}
