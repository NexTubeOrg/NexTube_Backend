using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Constants;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Videos.Commands.UploadVideo
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, int>
    {
        private readonly IVideoService _videoService;
        private readonly IPhotoService _photoService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationDbContext _dbContext;

        public UploadVideoCommandHandler(IVideoService videoService, IPhotoService photoService, IDateTimeService dateTimeService, IApplicationDbContext dbContext)
        {
            _videoService = videoService;
            _photoService = photoService;
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
        }

        public async Task<int> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var videoUploadResult = await _videoService.UploadVideoAsync(request.Source);
            var photoUploadResult = await _photoService.UploadPhoto(request.PreviewPhotoSource);
            var accessModificator = await _dbContext.VideoAccessModificators.Where(v => v.Modificator == request.AccessModificator).FirstOrDefaultAsync();

            if (accessModificator == null)
            {
                throw new NotFoundException(request.AccessModificator, nameof(VideoAccessModificatorEntity));
            }

            var video = new VideoEntity()
            {
                Name = request.Name,
                Description = request.Description,
                VideoFileId = Guid.Parse(videoUploadResult.VideoFileId),
                PreviewPhotoFileId = Guid.Parse(photoUploadResult.PhotoId),
                Creator = request.Creator,
                AccessModificator =  accessModificator,
                DateCreated = _dateTimeService.Now,
            };

            await _dbContext.Videos.AddAsync(video);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return video.Id;
        }
    }
}
