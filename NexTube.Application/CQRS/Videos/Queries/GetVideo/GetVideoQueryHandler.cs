using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Constants;
using NexTube.Domain.Entities;
using WebShop.Application.Common.Exceptions;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideo {
    public class GetVideoQueryHandler : IRequestHandler<GetVideoQuery, GetVideoQueryResult> {
        private readonly IApplicationDbContext _dbContext;
        private readonly IDateTimeService _dateTimeService;

        public GetVideoQueryHandler(IApplicationDbContext dbContext, IDateTimeService dateTimeService)
        {
            _dbContext = dbContext;
            _dateTimeService = dateTimeService;
        }

        public async Task<GetVideoQueryResult> Handle(GetVideoQuery request, CancellationToken cancellationToken) {
            var videoLookup = await _dbContext.Videos
                .Where(v => v.Id == request.VideoId)
                .Include(e => e.Creator)
                .Select(v => new VideoLookup() {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    VideoFile = v.VideoFileId,
                    AccessModificator = v.AccessModificator.Modificator,
                    PreviewPhotoFile = v.PreviewPhotoFileId,
                    DateCreated = v.DateCreated,
                    Views = v.Views,
                    Creator = new UserLookup() {
                        UserId = v.Creator.Id,
                        FirstName = v.Creator.FirstName,
                        LastName = v.Creator.LastName,
                        ChannelPhoto = v.Creator.ChannelPhotoFileId.ToString(),
                    },
                }).FirstOrDefaultAsync();

            if (videoLookup == null) {
                throw new NotFoundException(request.VideoId.ToString(), nameof(VideoEntity));
            }

            if (videoLookup.AccessModificator == VideoAccessModificators.Private && videoLookup.Creator?.UserId != request.RequesterId) {
                throw new ForbiddenAccessException();
            }

            if (request.RequesterId is not null && request.RequesterId != -1)
            {
                _dbContext.UserVideoHistories.Add(new UserVideoHistoryEntity()
                {
                    UserId = request.RequesterId.Value,
                    VideoId = videoLookup.Id!.Value,
                    DateWatched = _dateTimeService.Now,
                });

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            var GetVideoEntityQueryResult = new GetVideoQueryResult() {
                Video = videoLookup
            };

            return GetVideoEntityQueryResult;
        }
    }
}
