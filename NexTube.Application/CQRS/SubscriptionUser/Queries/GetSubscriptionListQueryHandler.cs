using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.SubscriptionUser.Queries;
using NexTube.Application.Models.Lookups;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class GetSubscriptionListQueryHandler : IRequestHandler<GetSubscriptionListQuery, GetSubscriptionsListQueryResult>
{
    private readonly IApplicationDbContext _context;

    public GetSubscriptionListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetSubscriptionsListQueryResult> Handle(GetSubscriptionListQuery request, CancellationToken cancellationToken)
    {

        var subscriptions = await _context.Subscriptions
            .Where(s => s.Creator.Id == request.SubscriptionUserTo)
            .Select(c => new SubscriptionLookup()
            {
                UserId = c.Subscriber.Id,
                FirstName = c.Subscriber.FirstName,
                LastName = c.Subscriber.LastName,
                ChannelPhotoFileId = c.Subscriber.ChannelPhotoFileId.ToString(),
            
            })
            .ToListAsync();



        return new GetSubscriptionsListQueryResult()
        {
            Subscriptions = subscriptions
        };
    }
}