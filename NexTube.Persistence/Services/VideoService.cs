using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Videos.Queries.GetCommentsList;
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

        public async Task<(Result Result, IList<CommentLookup> Comments)> GetCommentsListAsync(int? videoId) {
            var query = _videoDbContext.Comments
                .Where(c => c.VideoEntity.Id == videoId)
                .Select(c=> new CommentLookup() {
                    Content = c.Content,
                });

            var comments = await query.ToListAsync();

            return (Result.Success(), comments);
        }
    }
}
