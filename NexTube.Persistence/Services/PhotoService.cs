using Minio.DataModel.Args;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Persistence.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IFileService _fileService;

        public PhotoService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<(Result Result, Stream Stream, string ContentType)> GetPhoto(string photoId)
        {
            var getPhoto = await _fileService.GetFileStreamAsync("photos", photoId);
            return getPhoto;
        }

        public async Task<(Result Result, string? PhotoId)> UploadPhoto(Stream source)
        {
            var uploadPhoto = await _fileService.UploadFileAsync("photos", source);
            return uploadPhoto;
        }
    }
}
