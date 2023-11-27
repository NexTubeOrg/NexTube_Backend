using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Constants;

namespace NexTube.Application.CQRS.Videos.Queries.GetAllVideos
{
    public class GetAllVideosQueryHandler : IRequestHandler<GetAllVideosQuery, GetAllVideosQueryResult>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IVideoAccessModificatorService _videoAccessModificatorService;

        public GetAllVideosQueryHandler(IApplicationDbContext dbContext, IVideoAccessModificatorService videoAccessModificatorService)
        {
            _dbContext = dbContext;
            _videoAccessModificatorService = videoAccessModificatorService;
        }

        public async Task<GetAllVideosQueryResult> Handle(GetAllVideosQuery request, CancellationToken cancellationToken)
        {
            var videoLookups = await _dbContext.Videos
               .Include(e => e.Creator)
               .Where(v => v.AccessModificator.Modificator == VideoAccessModificators.Public || v.Creator.Id == request.RequesterId)
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

            var GetAllVideoEntitiesQueryResult = new GetAllVideosQueryResult()
            {
                VideoEntities = videoLookups,
            };

            return GetAllVideoEntitiesQueryResult;
        }
    }
}
