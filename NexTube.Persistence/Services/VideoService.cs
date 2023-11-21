using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;
using NexTube.Infrastructure.Services;
using NexTube.Persistence.Data.Contexts;

namespace NexTube.Persistence.Services
{
    public class VideoService : IVideoService {
        private readonly IFileService _fileService;

        public VideoService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<(Result Result, string VideoFileId)> UploadVideo(Stream source) {
            var uploadVideo = await _fileService.UploadFileAsync("videos", source);
            return (uploadVideo.Result, uploadVideo.FileId);
        }

        public async Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoFileId) {
            var getVideo = await _fileService.GetFileUrlAsync("videos", videoFileId, "video/mp4");
            return (getVideo.Result, getVideo.Url);
        }


        public Task<Result> DeleteVideoFileById(string videoFileId)
        {
            throw new NotImplementedException();
        }
    }
}
