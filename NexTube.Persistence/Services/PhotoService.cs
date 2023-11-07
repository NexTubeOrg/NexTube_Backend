using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;

namespace NexTube.Persistence.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IFileService _fileService;

        public PhotoService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<(Result Result, string Url)> GetPhotoUrl(string photoId)
        {
            var getPhotoUrl = await _fileService.GetFileUrlAsync("photos", photoId, "image/png");
            return getPhotoUrl;
        }

        public async Task<(Result Result, string? PhotoId)> UploadPhoto(Stream source)
        {
            var uploadPhoto = await _fileService.UploadFileAsync("photos", source);
            return uploadPhoto;
        }
    }
}
