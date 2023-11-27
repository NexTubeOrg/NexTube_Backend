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

        public DeleteVideoCommandHandler(IVideoService videoService, IApplicationDbContext dbContext)
        {
            _videoService = videoService;
            _dbContext = dbContext;
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

            if (videoEntity.Creator.Id != request.RequsterId)
            {
                throw new ForbiddenAccessException();
            }

            _dbContext.Videos.Remove(videoEntity);
            await _videoService.DeleteVideoFileById(videoEntity.VideoFileId.ToString());

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
