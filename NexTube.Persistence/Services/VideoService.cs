using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
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

        public async Task<(Result Result, int VideoEntityId)> UploadVideo(string name, string description, Stream previewPhotoSource, Stream source)
        {
            var uploadVideo = await _fileService.UploadFileAsync("videos", source);
            var uploadPhoto = await _photoService.UploadPhoto(previewPhotoSource);

            var videoEntity = new VideoEntity()
            {
                Name = name,
                Description = description,
                VideoId = Guid.Parse(uploadVideo.FileId),
                PreviewPhotoId = Guid.Parse(uploadPhoto.PhotoId),
                DateOfUpload = _dateTimeService.Now
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
    }
}
