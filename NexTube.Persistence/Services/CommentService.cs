using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;
using NexTube.Persistence.Data.Contexts;

namespace NexTube.Persistence.Services
{
    public class CommentService : IVideoCommentService {
        private readonly IDateTimeService _dateTimeService;
        private readonly ApplicationDbContext _dbContext;

        public CommentService(IDateTimeService dateTimeService, ApplicationDbContext dbContext) {
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
        }
        public async Task<Result> AddCommentAsync(int? videoId, string content, ApplicationUser creator) {
            var video = await _dbContext.Videos.FindAsync(videoId);

            if (video is null)
                throw new NotFoundException(videoId.ToString(), nameof(VideoEntity));

            var comment = new VideoCommentEntity() {
                Content = content,
                VideoEntity = video,
                Creator = creator,
                DateCreated = _dateTimeService.Now,
            };

            _dbContext.VideoComments.Add(comment);
            await _dbContext.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<(Result Result, IList<CommentLookup> Comments)> GetCommentsListAsync(int? videoId, int page, int pageSize) {
            var query = _dbContext.VideoComments
                .Where(c => c.VideoEntity.Id == videoId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CommentLookup() {
                    CommentId = c.Id,
                    Content = c.Content,
                    DateCreated = c.DateCreated,
                    Creator = new UserLookup() {
                        UserId = c.Creator.Id,
                        FirstName = c.Creator.FirstName,
                        LastName = c.Creator.LastName
                    }
                })
                .OrderByDescending(c => c.DateCreated);

            var comments = await query.ToListAsync();

            return (Result.Success(), comments);
        }

        public async Task<Result> DeleteCommentAsync(int? commentId) {
            var comment = await _dbContext.VideoComments.FindAsync(commentId);

            if (comment is null)
                throw new NotFoundException(commentId.ToString(), nameof(VideoCommentEntity));

            _dbContext.VideoComments.Remove(comment);

            await _dbContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
