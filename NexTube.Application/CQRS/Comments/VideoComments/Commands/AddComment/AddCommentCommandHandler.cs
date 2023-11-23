using Ardalis.GuardClauses;
using MediatR;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment {
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, CommentLookup> {
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationDbContext _dbContext;
        public AddCommentCommandHandler(IDateTimeService dateTimeService, IApplicationDbContext dbContext) {
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
        }

        public async Task<CommentLookup> Handle(AddCommentCommand request, CancellationToken cancellationToken) {
            var video = await _dbContext.Videos.FindAsync(request.VideoId);

            if (video is null)
                throw new NotFoundException(request.VideoId.ToString(), nameof(VideoEntity));

            var comment = new VideoCommentEntity() {
                Content = request.Content,
                VideoEntity = video,
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
