using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;
using WebShop.Application.Common.Exceptions;

namespace NexTube.Application.CQRS.Videos.Commands.DeleteVideo
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand>
    {
        private readonly IVideoService _videoService;
        private readonly IApplicationDbContext _dbContext;
        private readonly IPhotoService _photoService;

        public DeleteVideoCommandHandler(IVideoService videoService, IApplicationDbContext dbContext, IPhotoService photoService)
        {
            _videoService = videoService;
            _dbContext = dbContext;
            _photoService = photoService;
        }

        public async Task Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var videoEntity = await _dbContext.Videos
                .Include(u => u.Creator)
                .Where(e => e.Id == request.VideoId)
                .FirstOrDefaultAsync();

            if (videoEntity == null)
            {
                throw new NotFoundException(request.VideoId.ToString(), nameof(VideoEntity));
            }

            if (videoEntity.Creator?.Id != request.RequesterId)
            {
                throw new ForbiddenAccessException();
            }

            _dbContext.Reports.RemoveRange(_dbContext.Reports.Where(c => c.Video.Id == videoEntity.Id));
            _dbContext.Videos.Remove(videoEntity);
            await _videoService.DeleteVideoAsync(videoEntity.VideoFileId.ToString());
            await _photoService.DeletePhotoAsync(videoEntity.PreviewPhotoFileId.ToString());

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
