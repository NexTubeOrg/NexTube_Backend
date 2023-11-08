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
        private readonly VideoDbContext _videoDbContext;

        public VideoService(IFileService fileService, IPhotoService photoService, IDateTimeService dateTimeService, VideoDbContext videoDbContext)
        {
            _fileService = fileService;
            _photoService = photoService;
            _dateTimeService = dateTimeService;
            _videoDbContext = videoDbContext;
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

            _videoDbContext.Videos.Add(videoEntity);
            _videoDbContext.SaveChanges();

            return (uploadVideo.Result, videoEntity.Id);
        }

        public async Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoId)
        {
            var getVideo = await _fileService.GetFileUrlAsync("videos", videoId);

            return (getVideo.Result, getVideo.Url);
        }

        public async Task<(Result Result, VideoEntity VideoEntity)> GetVideoEntity(int videoEntityId)
        {
            var videoEntity = await _videoDbContext.Videos.Where(e => e.Id == videoEntityId).FirstAsync();
            return (Result.Success(), videoEntity);
        }
    }
}
