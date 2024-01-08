using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Notifications.Queries.GetUserNotifications {
    public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, IList<NotificationLookup>> {
        private readonly IApplicationDbContext _dbContext;

        public GetUserNotificationsQueryHandler(IApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<IList<NotificationLookup>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken) {
            var result = await _dbContext.Subscriptions
                .Where(s => s.CreatorId == request.UserId) // get user subscriptions
                .Join(_dbContext.Notifications,
                    s => s.SubscriberId,
                    n => n.NotificationIssuerId,
                    (s, n) => n)
                .Include(n => n.NotificationData)
                .OrderByDescending(n => n.DateCreated)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(n => new NotificationLookup() {
                    Type = n.Type,
                    NotificationData = new VideoLookup() {
                        Name = n.NotificationData!.Name,
                        Id = n.NotificationData!.Id,
                        PreviewPhotoFile = n.NotificationData!.PreviewPhotoFileId
                    },
                    NotificationIssuer = new UserLookup() {
                        FirstName = n.NotificationIssuer!.FirstName,
                        LastName = n.NotificationIssuer!.LastName,
                        ChannelPhoto = n.NotificationIssuer.ChannelPhotoFileId.ToString()
                    },
                    DateCreated = n.DateCreated
                })
                .ToListAsync();
            return result;
        }
    }
}
