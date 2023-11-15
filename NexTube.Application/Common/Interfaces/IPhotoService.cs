using NexTube.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.Common.Interfaces
{
    public interface IPhotoService
    {
        Task<(Result Result, string? PhotoId)> UploadPhoto(Stream source);
        Task<(Result Result, string Url)> GetPhotoUrl(string photoId);
        Task<(Result Result, string Url)> GetPhotoUrl(string photoId, int size);
    }
}
