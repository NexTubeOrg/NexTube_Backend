using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;
using NexTube.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Persistence.Services
{
    public class VideoService : IVideoService
    {
        private readonly IFileService _fileService;
        private readonly VideoDbContext _videoDbContext;

        public VideoService(IFileService fileService, VideoDbContext videoDbContext)
        {
            _fileService = fileService;
            _videoDbContext = videoDbContext;
        }

        public async Task<(Result Result, string VideoId)> UploadVideo(string name, string description, Stream source)
        {
            var uploadVideo = await _fileService.UploadFileAsync("videos", source);

            var videoEntity = new VideoEntity()
            {
                Name = name,
                Description = description,
                VideoId = Guid.Parse(uploadVideo.FileId)
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
    }
}
