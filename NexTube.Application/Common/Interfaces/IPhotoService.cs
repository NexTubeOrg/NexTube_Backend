using NexTube.Application.Common.Models;
using System.Drawing;

namespace NexTube.Application.Common.Interfaces
{
    public interface IPhotoService
    {
        Task<(Result Result, string? PhotoId)> UploadPhoto(Stream source);
        Task<(Result Result, string Url)> GetPhotoUrl(string photoId);
        Task<(Result Result, string Url)> GetPhotoUrl(string photoId, int size);
        Task<(Result Result, Size Dimensions)> GetPhotoDimensionsAsync(Stream source);
        Task<bool> IsFileImageAsync(Stream source);
        Task DeletePhotoAsync(string photoId);
    }
}
