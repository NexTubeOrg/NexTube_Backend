using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Videos.Queries.GetCommentsList;

namespace NexTube.Application.Common.Interfaces
{
    public interface IVideoService
    {
        Task<(Result Result, string VideoId)> UploadVideo(string name, string description, Stream previewPhotoSource, Stream source);
        Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoId);
        Task<Result> AddCommentAsync(int? videoId, int? authorUserId, string content);
        Task<(Result Result, IList<CommentLookup> Comments)> GetCommentsListAsync(int? videoId);
    }
}
