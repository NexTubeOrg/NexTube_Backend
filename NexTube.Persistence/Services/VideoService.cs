using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;
using NexTube.Application.CQRS.Videos.Queries.GetCommentsList;
using NexTube.Domain.Entities;
using NexTube.Persistence.Data.Contexts;

namespace NexTube.Persistence.Services
{
    public class VideoService : IVideoService
    {
        private readonly IFileService _fileService;
        private readonly IPhotoService _photoService;
        private readonly IDateTimeService _dateTimeService;
        private readonly ApplicationDbContext _dbContext;

        public VideoService(IFileService fileService, IPhotoService photoService, IDateTimeService dateTimeService, ApplicationDbContext dbContext)
        {
            _fileService = fileService;
            _photoService = photoService;
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
        }

        public async Task<(Result Result, int VideoEntityId)> UploadVideo(string name, string description, Stream previewPhotoSource, Stream source, ApplicationUser creator)
        {
            var uploadVideo = await _fileService.UploadFileAsync("videos", source);
            var uploadPhoto = await _photoService.UploadPhoto(previewPhotoSource);

            var videoEntity = new VideoEntity()
            {
                Name = name,
                Description = description,
                VideoId = Guid.Parse(uploadVideo.FileId),
                PreviewPhotoId = Guid.Parse(uploadPhoto.PhotoId),
                DateCreated = _dateTimeService.Now,
                Creator = creator,
            };

            _dbContext.Videos.Add(videoEntity);
            await _dbContext.SaveChangesAsync();

            return (uploadVideo.Result, videoEntity.Id);
        }

        public async Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoId)
        {
            var getVideo = await _fileService.GetFileUrlAsync("videos", videoId, "video/mp4");

            return (getVideo.Result, getVideo.Url);
        }

        public async Task<(Result Result, VideoEntity VideoEntity)> GetVideoEntity(int videoEntityId)
        {
            var videoEntity = await _dbContext.Videos.Where(e => e.Id == videoEntityId).FirstOrDefaultAsync();

            if (videoEntity == null)
            {
                throw new NotFoundException(videoEntityId.ToString(), nameof(VideoEntity));
            }

            return (Result.Success(), videoEntity);
        }

        public async Task<(Result Result, IEnumerable<VideoEntity> VideoEntities)> GetAllVideoEntities()
        {
            var videoEntities = await _dbContext.Videos.ToListAsync();

            return (Result.Success(), videoEntities);
        }

        public async Task<Result> RemoveVideoByEntityId(int videoEntityId)
        {
            var videoEntity = await _dbContext.Videos.Where(e => e.Id == videoEntityId).FirstOrDefaultAsync();

            if (videoEntity == null)
            {
                throw new NotFoundException(videoEntityId.ToString(), nameof(VideoEntity));
            }

            _dbContext.Videos.Remove(videoEntity);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
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

        public async Task<(Result Result, IList<CommentLookup> Comments)> GetCommentsListAsync(int? videoId) {
            var query = _dbContext.VideoComments
                .Where(c => c.VideoEntity.Id == videoId)
                .Select(c=> new CommentLookup() {
                    Content = c.Content,
                    DateCreated = c.DateCreated,
                    Creator = new UserLookup() {
                        UserId = c.Creator.Id,
                        FirstName = c.Creator.FirstName,
                        LastName = c.Creator.FirstName
                    }
                });

            var comments = await query.ToListAsync();

            return (Result.Success(), comments);
        }
    }
}
