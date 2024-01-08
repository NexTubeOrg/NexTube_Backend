using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Constants;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoList
{
    public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, GetVideoListQueryResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetVideoListQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetVideoListQueryResult> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
        {
            var videoLookups = await _dbContext.Videos
               .Where(v => v.AccessModificator.Modificator == VideoAccessModificators.Public)
               .Where(v => request.Name != null ? v.Name.Contains(request.Name) : true)
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
                   DateModified = v.DateModified,
                   Creator = new UserLookup()
                   {
                       UserId = v.Creator.Id,
                       FirstName = v.Creator.FirstName,
                       LastName = v.Creator.LastName,
                       ChannelPhoto = v.Creator.ChannelPhotoFileId.ToString(),
                   }
               })
               .ToListAsync();

            var GetAllVideoEntitiesQueryResult = new GetVideoListQueryResult()
            {
                Videos = videoLookups,
            };

            return GetAllVideoEntitiesQueryResult;
        }
    }
}
