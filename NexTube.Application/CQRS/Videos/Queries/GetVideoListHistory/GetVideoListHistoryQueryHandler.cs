using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.UserVideoHistories.Queries.GetUserVideoHistoryList;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoListHistory
{
    public class GetVideoListHistoryQueryHandler : IRequestHandler<GetVideoListHistoryQuery, GetVideoListHistoryQueryResult>
    {
        private IApplicationDbContext _dbContext;

        public GetVideoListHistoryQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetVideoListHistoryQueryResult> Handle(GetVideoListHistoryQuery request, CancellationToken cancellationToken)
        {
            var query = (from video in _dbContext.Videos
                         join history in _dbContext.UserVideoHistories on video.Id equals history.VideoId
                         where history.UserId == request.RequesterId
                         orderby history.DateWatched descending
                         select new VideoLookup()
                         {
                             Id = video.Id,
                             Name = video.Name,
                             Description = video.Description,
                             VideoFile = video.VideoFileId,
                             AccessModificator = video.AccessModificator.Modificator,
                             PreviewPhotoFile = video.PreviewPhotoFileId,
                             DateCreated = video.DateCreated,
                             Views = video.Views,
                             DateModified = video.DateModified,
                             Creator = new UserLookup()
                             {
                                 UserId = video.Creator.Id,
                                 FirstName = video.Creator.FirstName,
                                 LastName = video.Creator.LastName,
                                 ChannelPhoto = video.Creator.ChannelPhotoFileId.ToString(),
                             }
                         })
                         .Skip((request.Page - 1) * request.PageSize)
                         .Take(request.PageSize);

            var result = await query.ToListAsync();

            return new GetVideoListHistoryQueryResult() { Videos = result };
        }
    }
}
