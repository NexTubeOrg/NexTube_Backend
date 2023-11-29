using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentRepliesList {
    public class GetCommentRepliesListQueryHandler : IRequestHandler<GetCommentRepliesListQuery, GetCommentRepliesListQueryResult> {
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationDbContext _dbContext;

        public GetCommentRepliesListQueryHandler(IDateTimeService dateTimeService, IApplicationDbContext dbContext) {
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
        }

        public async Task<GetCommentRepliesListQueryResult> Handle(GetCommentRepliesListQuery request, CancellationToken cancellationToken) {
            var query = _dbContext.VideoComments
                .Where(c => c.VideoEntity.Id == request.VideoId && c.RepliedTo.Id == request.RootCommentId)
                .OrderBy(c => c.DateCreated)
                .Include(c => c.Creator)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CommentLookup() {
                    CommentId = c.Id,
                    Content = c.Content,
                    DateCreated = c.DateCreated,
                    Creator = new UserLookup() {
                        UserId = c.Creator.Id,
                        FirstName = c.Creator.FirstName,
                        LastName = c.Creator.LastName,
                        ChannelPhoto = c.Creator.ChannelPhotoFileId.ToString()
                    }
                });

            var comments = await query.ToListAsync();

            return new GetCommentRepliesListQueryResult() {
                Comments = comments
            };
        }
    }
}
