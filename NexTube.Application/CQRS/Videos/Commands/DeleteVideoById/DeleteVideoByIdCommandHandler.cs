using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Videos.Commands.DeleteVideoById
{
    public class DeleteVideoByIdCommandHandler : IRequestHandler<DeleteVideoByIdCommand>
    {
        private readonly IVideoService _videoService;
        private readonly IApplicationDbContext _dbContext;

        public DeleteVideoByIdCommandHandler(IVideoService videoService, IApplicationDbContext dbContext)
        {
            _videoService = videoService;
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteVideoByIdCommand request, CancellationToken cancellationToken)
        {
            var videoEntity = await _dbContext.Videos.Where(e => e.Id == request.VideoId).FirstOrDefaultAsync();

            if (videoEntity == null)
            {
                throw new NotFoundException(request.VideoId.ToString(), nameof(VideoEntity));
            }

            _dbContext.Videos.Remove(videoEntity);
            await _videoService.DeleteVideoFileById(videoEntity.VideoFileId.ToString());

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
