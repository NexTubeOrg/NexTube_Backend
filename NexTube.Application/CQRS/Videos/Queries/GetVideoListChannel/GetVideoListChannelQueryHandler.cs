using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Constants;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoListChannel
{
    public class GetVideoListChannelQueryHandler : IRequestHandler<GetVideoListChannelQuery, GetVideoListChannelQueryResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetVideoListChannelQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetVideoListChannelQueryResult> Handle(GetVideoListChannelQuery request, CancellationToken cancellationToken)
        {
            var videoLookups = new List<VideoLookup>();

            if (request.ChannelId == request.RequesterId)
            {
                videoLookups = await _dbContext.Videos
                   .Where(v => v.Creator.Id == request.ChannelId)
                   .OrderByDescending(c => c.DateCreated)
                   .Include(e => e.Creator)
                   .Skip((request.Page - 1) * request.PageSize)
                   .Take(request.PageSize)
                   .Select(v => new VideoLookup()
                   {
                       Id = v.Id,
                       Name = v.Name,
                       Description = v.Description,
                       VideoFile = v.VideoFileId,
                       AccessModificator = v.AccessModificator.Modificator,
                       PreviewPhotoFile = v.PreviewPhotoFileId,
                       DateCreated = v.DateCreated,
                       Views = v.Views,
                       Creator = new UserLookup()
                       {
                           UserId = v.Creator.Id,
                           FirstName = v.Creator.FirstName,
                           LastName = v.Creator.LastName,
                           ChannelPhoto = v.Creator.ChannelPhotoFileId.ToString(),
                       }
                   })
                   .ToListAsync();
            }
            else
            {
                videoLookups = await _dbContext.Videos
                   .Where(v => v.Creator.Id == request.ChannelId && v.AccessModificator.Modificator == VideoAccessModificators.Public)
                   .OrderByDescending(c => c.DateCreated)
                   .Include(e => e.Creator)
                   .Skip((request.Page - 1) * request.PageSize)
                   .Take(request.PageSize)
                   .Select(v => new VideoLookup()
                   {
                       Id = v.Id,
                       Name = v.Name,
                       Description = v.Description,
                       VideoFile = v.VideoFileId,
                       AccessModificator = v.AccessModificator.Modificator,
                       PreviewPhotoFile = v.PreviewPhotoFileId,
                       DateCreated = v.DateCreated,
                       Views = v.Views,
                       Creator = new UserLookup()
                       {
                           UserId = v.Creator.Id,
                           FirstName = v.Creator.FirstName,
                           LastName = v.Creator.LastName,
                           ChannelPhoto = v.Creator.ChannelPhotoFileId.ToString(),
                       }
                   })
                   .ToListAsync();
            }

            var result = new GetVideoListChannelQueryResult()
            {
                Videos = videoLookups
            };

            return result;
        }
    }
}
