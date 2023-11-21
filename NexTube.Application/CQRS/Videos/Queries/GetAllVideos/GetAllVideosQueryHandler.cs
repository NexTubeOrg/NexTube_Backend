using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Videos.Queries.GetAllVideos
{
    public class GetAllVideosQueryHandler : IRequestHandler<GetAllVideosQuery, GetAllVideosQueryResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetAllVideosQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllVideosQueryResult> Handle(GetAllVideosQuery request, CancellationToken cancellationToken)
        {
            var videoLookups = await _dbContext.Videos
               .Include(e => e.Creator)
               .Select(v => new VideoLookup()
               {
                   Id = v.Id,
                   Name = v.Name,
                   Description = v.Description,
                   VideoFile = v.VideoId,
                   PreviewPhotoFile = v.PreviewPhotoId,
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
