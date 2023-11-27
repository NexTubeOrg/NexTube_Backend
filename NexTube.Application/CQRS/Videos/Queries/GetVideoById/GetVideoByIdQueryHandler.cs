using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Constants;
using NexTube.Domain.Entities;
using WebShop.Application.Common.Exceptions;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoById
{
    public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, GetVideoByIdQueryResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetVideoByIdQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetVideoByIdQueryResult> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            var videoLookup = await _dbContext.Videos
                .Where(v => v.Id == request.VideoId)
                .Include(e => e.Creator)
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
                }).FirstOrDefaultAsync();

            if (videoLookup == null)
            {
                throw new NotFoundException(request.VideoId.ToString(), nameof(VideoEntity));
            }

            if (videoLookup.AccessModificator == VideoAccessModificators.Private && videoLookup.Creator?.UserId != request.RequsterId)
            {
                throw new ForbiddenAccessException();
            }

            var GetVideoEntityQueryResult = new GetVideoByIdQueryResult()
            {
                Video = videoLookup
            };

            return GetVideoEntityQueryResult;
        }
    }
}
