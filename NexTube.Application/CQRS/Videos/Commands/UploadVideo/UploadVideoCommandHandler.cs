using MediatR;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Videos.Commands.UploadVideo
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, int>
    {
        private readonly IVideoService _videoService;
        private readonly IPhotoService _photoService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationDbContext _dbContext;
        private readonly IVideoAccessModificatorService _videoAccessModificatorService;

        public UploadVideoCommandHandler(
            IVideoService videoService,
            IPhotoService photoService,
            IDateTimeService dateTimeService,
            IApplicationDbContext dbContext,
            IVideoAccessModificatorService videoAccessModificatorService)
        {
            _videoService = videoService;
            _photoService = photoService;
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
            _videoAccessModificatorService = videoAccessModificatorService;
        }

        public async Task<int> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var videoUploadResult = await _videoService.UploadVideo(request.Source);
            var photoUploadResult = await _photoService.UploadPhoto(request.PreviewPhotoSource);

            var video = new VideoEntity()
            {
                Name = request.Name,
                Description = request.Description,
                VideoFileId = Guid.Parse(videoUploadResult.VideoFileId),
                PreviewPhotoFileId = Guid.Parse(photoUploadResult.PhotoId),
                Creator = request.Creator,
                AccessModificator =  await _videoAccessModificatorService.GetPublicAccessModificatorAsync(),
                DateCreated = _dateTimeService.Now,
            };

            await _dbContext.Videos.AddAsync(video);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return video.Id;
        }
    }
}
