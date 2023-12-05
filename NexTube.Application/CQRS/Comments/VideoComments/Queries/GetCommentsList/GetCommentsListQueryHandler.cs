using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetVideoCommentsCount;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList {
    public class GetCommentsListQueryHandler : IRequestHandler<GetCommentsListQuery, GetCommentsListQueryResult> {
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public GetCommentsListQueryHandler(IDateTimeService dateTimeService, IApplicationDbContext dbContext, IMediator mediator) {
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<GetCommentsListQueryResult> Handle(GetCommentsListQuery request, CancellationToken cancellationToken) {
            var query = _dbContext.VideoComments
                .Where(c => c.RepliedTo == null && c.VideoEntity.Id == request.VideoId)
                .OrderByDescending(c => c.DateCreated)
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
                    },
                    RepliesCount = _dbContext.VideoComments.Count(v => v.RepliedTo.Id == c.Id)
                });

            var comments = await query.ToListAsync();

            var totalCount = await _mediator.Send(new GetVideoCommentsCountQuery() {
                VideoId = request.VideoId.Value
            });

            return new GetCommentsListQueryResult() {
                Comments = comments,
                TotalCount = totalCount
            };
        }
    }
}
