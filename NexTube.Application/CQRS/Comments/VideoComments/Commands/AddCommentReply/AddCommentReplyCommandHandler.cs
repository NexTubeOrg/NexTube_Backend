using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.AddCommentReply {
    public class AddCommentReplyCommandHandler : IRequestHandler<AddCommentReplyCommand, CommentLookup> {
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationDbContext _dbContext;
        public AddCommentReplyCommandHandler(IDateTimeService dateTimeService, IApplicationDbContext dbContext) {
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
        }

        public async Task<CommentLookup> Handle(AddCommentReplyCommand request, CancellationToken cancellationToken) {
            var rootComment = await _dbContext.VideoComments
                .Where(c => c.Id == request.ReplyToCommentId)
                .Include(c => c.VideoEntity)
                .Include(c => c.RepliedTo)
                .FirstOrDefaultAsync();

            if (rootComment is null)
                throw new NotFoundException(request.ReplyToCommentId.ToString(), nameof(VideoCommentEntity));

            // forbid adding replies to replies
            if (rootComment.RepliedTo != null)
                rootComment = rootComment.RepliedTo;

            var comment = new VideoCommentEntity() {
                Content = request.Content,
                VideoEntity = rootComment.VideoEntity,
                RepliedTo = rootComment,
                Creator = request.Creator,
                DateCreated = _dateTimeService.Now,
            };

            _dbContext.VideoComments.Add(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommentLookup() {
                CommentId = comment.Id,
                Content = comment.Content,
                Creator = new UserLookup() {
                    ChannelPhoto = comment.Creator.ChannelPhotoFileId.ToString(),
                    FirstName = comment.Creator.FirstName,
                    LastName = comment.Creator.LastName,
                    UserId = comment.Creator.Id,
                },
                DateCreated = comment.DateCreated,
            };
        }
    }
}
