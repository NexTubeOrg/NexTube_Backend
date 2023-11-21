using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.Common.Interfaces
{
    public interface IVideoService
    {
        Task<(Result Result, string VideoFileId)> UploadVideo(Stream source);
        Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoFileId);
        Task<Result> DeleteVideoFileById(string videoFileId);
    }
}
