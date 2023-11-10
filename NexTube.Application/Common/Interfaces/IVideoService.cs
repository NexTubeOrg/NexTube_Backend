using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;

namespace NexTube.Application.Common.Interfaces
{
    public interface IVideoService
    {
        Task<(Result Result, int VideoEntityId)> UploadVideo(string name, string description, Stream previewPhotoSource, Stream source);
        Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoId);
        Task<(Result Result, VideoEntity VideoEntity)> GetVideoEntity(int videoEntityId);
        Task<(Result Result, IEnumerable<VideoEntity> VideoEntities)> GetAllVideoEntities();
    }
}
