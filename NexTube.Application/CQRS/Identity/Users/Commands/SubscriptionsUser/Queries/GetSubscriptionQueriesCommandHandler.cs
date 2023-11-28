using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList;
using NexTube.Application.Models.Lookups;
using NexTube.Application.Subscriptions.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class GetSubscriptionQueriesCommandHandler : IRequestHandler<GetSubscriptionQueriesCommand, GetSubscriptionsListQueryResult>
{
    private readonly IApplicationDbContext _context;

    public GetSubscriptionQueriesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetSubscriptionsListQueryResult> Handle(GetSubscriptionQueriesCommand request, CancellationToken cancellationToken)
    {
        
        var subscriptions = await _context.Subscriptions
            .Where(s => s.User.Id == request.SubscriptionUserTo)
            .Select(c => new SubscriptionLookup()
            {
                 
                DateCreated = c.DateCreated,
                Subscription = new UserLookup()
                {
                    UserId = c.Subscriber.Id,
                    FirstName = c.Subscriber.FirstName,
                    LastName = c.Subscriber.LastName,
                    ChannelPhoto = c.Subscriber.ChannelPhotoFileId.ToString()
                }
            })
            .ToListAsync();

     

        return new GetSubscriptionsListQueryResult()
        {
            Subscriptions = subscriptions
        };
    }
}
