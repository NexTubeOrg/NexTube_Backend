using NexTube.Application.Common.Models;

namespace NexTube.Application.Common.Interfaces
{
    public interface IVideoService
    {
        Task<(Result Result, string VideoId)> UploadVideo(string name, string description, Stream previewPhotoSource, Stream source);
        Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoId);
    }
}
