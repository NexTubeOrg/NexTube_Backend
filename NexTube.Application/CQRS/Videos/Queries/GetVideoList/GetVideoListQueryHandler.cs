using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Constants;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoList
{
    public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, GetVideoListQueryResult>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IVideoAccessModificatorService _videoAccessModificatorService;

        public GetVideoListQueryHandler(IApplicationDbContext dbContext, IVideoAccessModificatorService videoAccessModificatorService)
        {
            _dbContext = dbContext;
            _videoAccessModificatorService = videoAccessModificatorService;
        }

        public async Task<GetVideoListQueryResult> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
        {
            var videoLookups = await _dbContext.Videos
               .Where(v => v.AccessModificator.Modificator == VideoAccessModificators.Public || v.Creator.Id == request.RequesterId)
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
                VideoEntities = videoLookups,
            };

            return GetAllVideoEntitiesQueryResult;
        }
    }
}
