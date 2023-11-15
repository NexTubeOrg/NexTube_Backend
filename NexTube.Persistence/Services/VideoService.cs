using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Files.Videos.Queries.Common;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;
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

        public async Task<(Result Result, VideoLookup VideoEntity)> GetVideoEntity(int videoEntityId)
        {
            var videoLookup = await _dbContext.Videos
                .Where(e => e.Id == videoEntityId)
                .Select(v => new VideoLookup() {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    VideoFile = v.VideoId,
                    PreviewPhotoFile = v.PreviewPhotoId,
                    DateCreated = v.DateCreated,
                    Creator = new UserLookup() { 
                        UserId = v.Creator.Id,
                        FirstName = v.Creator.FirstName,
                        LastName = v.Creator.LastName,
                    }
            }).FirstOrDefaultAsync();

            if (videoLookup == null)
            {
                throw new NotFoundException(videoEntityId.ToString(), nameof(VideoEntity));
            }

            return (Result.Success(), videoLookup);
        }

        public async Task<(Result Result, IEnumerable<VideoLookup> VideoEntities)> GetAllVideoEntities()
        {
            var videoLookup = await _dbContext.Videos
                .Select(v => new VideoLookup()
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    VideoFile = v.VideoId,
                    PreviewPhotoFile = v.PreviewPhotoId,
                    DateCreated = v.DateCreated,
                    Creator = new UserLookup()
                    {
                        UserId = v.Creator.Id,
                        FirstName = v.Creator.FirstName,
                        LastName = v.Creator.LastName,
                    }
                })
                .ToListAsync();

            return (Result.Success(), videoLookup);
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
    }
}
