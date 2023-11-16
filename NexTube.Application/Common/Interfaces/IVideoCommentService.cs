using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.Common.Interfaces
{
    public interface IVideoCommentService {
        Task<Result> AddCommentAsync(int? videoId, string content, ApplicationUser creator);
        Task<(Result Result, IList<CommentLookup> Comments)> GetCommentsListAsync(int? videoId, int page, int pageSize);
        Task<Result> DeleteCommentAsync(int? commentId);
    }
}
