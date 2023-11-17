using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.Common.Interfaces
{
    public interface IVideoService
    {
        Task<(Result Result, int VideoEntityId)> UploadVideo(string name, string description, Stream previewPhotoSource, Stream source, ApplicationUser creator);
        Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoId);
        Task<(Result Result, VideoLookup VideoEntity)> GetVideoEntity(int videoEntityId);
        Task<(Result Result, IEnumerable<VideoLookup> VideoEntities)> GetAllVideoEntities();
        Task<Result> RemoveVideoByEntityId(int videoEntityId);
    }
}
