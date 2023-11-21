using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoById
{
    public class GetVideoEntityQueryHandler : IRequestHandler<GetVideoByIdQuery, GetVideoEntityQueryResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetVideoEntityQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetVideoEntityQueryResult> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            var videoLookup = await _dbContext.Videos
                .Where(e => e.Id == request.VideoId)
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
                }).FirstOrDefaultAsync();

            if (videoLookup == null)
            {
                throw new NotFoundException(request.VideoId.ToString(), nameof(VideoEntity));
            }

            var GetVideoEntityQueryResult = new GetVideoEntityQueryResult()
            {
                Video = videoLookup
            };

            return GetVideoEntityQueryResult;
        }
    }
}
