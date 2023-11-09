using Ardalis.GuardClauses;
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
        private readonly VideoDbContext _videoDbContext;

        public VideoService(IFileService fileService, IPhotoService photoService, VideoDbContext videoDbContext)
        {
            _fileService = fileService;
            _photoService = photoService;
            _videoDbContext = videoDbContext;
        }

        public async Task<(Result Result, string VideoId)> UploadVideo(string name, string description, Stream previewPhotoSource, Stream source)
        {
            var uploadVideo = await _fileService.UploadFileAsync("videos", source);
            var uploadPhoto = await _photoService.UploadPhoto(previewPhotoSource);

            var videoEntity = new VideoEntity()
            {
                Name = name,
                Description = description,
                VideoId = Guid.Parse(uploadVideo.FileId),
                PreviewPhotoId = Guid.Parse(uploadPhoto.PhotoId)
            };

            _videoDbContext.Videos.Add(videoEntity);
            _videoDbContext.SaveChanges();

            return (uploadVideo.Result, uploadVideo.FileId);
        }

        public async Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoId)
        {
            var getVideo = await _fileService.GetFileUrlAsync("videos", videoId);

            return (getVideo.Result, getVideo.Url);
        }

        public async Task<Result> AddCommentAsync(int? videoId, int? authorUserId, string content) {
            var video = await _videoDbContext.Videos.FindAsync(videoId);

            if (video is null)
                throw new NotFoundException(videoId.ToString(), nameof(VideoEntity));

            var comment = new VideoCommentEntity() {
                Content = content,
                VideoEntity = video,
            };

            _videoDbContext.Comments.Add(comment);
            await _videoDbContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
