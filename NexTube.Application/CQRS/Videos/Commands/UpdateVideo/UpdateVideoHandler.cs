using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Videos.Commands.UpdateVideo
{
    public class UpdateVideoHandler : IRequestHandler<UpdateVideoCommand, VideoLookup>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IDateTimeService _dateTimeService;

        public UpdateVideoHandler(IApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _dbContext = context;
            _dateTimeService = dateTimeService;
        }

        public async Task<VideoLookup> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            var video = await _dbContext.Videos
                .Select(v => new VideoEntity
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    VideoFileId = v.VideoFileId,
                    AccessModificator = v.AccessModificator,
                    PreviewPhotoFileId = v.PreviewPhotoFileId,
                    DateCreated = v.DateCreated,
                    Creator = v.Creator
                })
                .Where(v => v.Id == request.VideoId)
                .FirstOrDefaultAsync();

            if (video == null)
            {
                throw new NotFoundException(request.VideoId.ToString(), nameof(VideoEntity));
            }

            video.Name = request.Name ?? video.Name;
            video.Description = request.Description ?? video.Description;
            video.DateModified = _dateTimeService.Now;

            if (request.AccessModificator != video.AccessModificator.Modificator)
            {
                var accessModificator = await _dbContext.VideoAccessModificators.Where(a => a.Modificator == request.AccessModificator).FirstOrDefaultAsync();

                if (accessModificator != null)
                {
                    video.AccessModificator = accessModificator;
                }
            }

            _dbContext.Videos.Update(video);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var videoLookup = new VideoLookup()
            {
                Id = video.Id,
                Name = video.Name,
                Description = video.Description,
                VideoFile = video.VideoFileId,
                AccessModificator = video.AccessModificator.Modificator,
                PreviewPhotoFile = video.PreviewPhotoFileId,
                DateCreated = video.DateCreated,
                DateModified = video.DateModified
            };

            return videoLookup;
        }
    }
}
