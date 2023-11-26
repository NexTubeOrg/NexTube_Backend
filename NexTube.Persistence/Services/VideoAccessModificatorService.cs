using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Constants;
using NexTube.Domain.Entities;
using WebShop.Application.Common.Exceptions;

namespace NexTube.Persistence.Services
{
    public class VideoAccessModificatorService : IVideoAccessModificatorService
    {
        private readonly IApplicationDbContext _dbContext;

        public VideoAccessModificatorService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAccessModificatorAsync(string modificatorName)
        {
            if(await _dbContext.VideoAccessModificators.FirstOrDefaultAsync(v => v.Modificator == modificatorName) != null)
            {
                throw new AlreadyExistsException(modificatorName, nameof(VideoAccessModificatorEntity));
            }

            var videoAccessModificator = new VideoAccessModificatorEntity()
            {
                Modificator = modificatorName,
            };

            await _dbContext.VideoAccessModificators.AddAsync(videoAccessModificator);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<bool> CanUserWatchVideo(int videoId, int userId)
        {
            var video = await _dbContext.Videos.FirstOrDefaultAsync(v => v.Id == videoId);

            if (video == null)
            {
                throw new NotFoundException(videoId.ToString(), nameof(VideoEntity));
            }

            if (video.AccessModificator.Modificator == VideoAccessModificators.Public)
            {
                return true;
            }

            if (video.AccessModificator.Modificator == VideoAccessModificators.Private)
            {
                if (video.Creator.Id == userId)
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        public async Task<VideoAccessModificatorEntity> GetPublicAccessModificatorAsync()
        {
            var publicAccessModificator = await _dbContext.VideoAccessModificators
                .Where(a => a.Modificator == VideoAccessModificators.Public)
                .FirstOrDefaultAsync();

            if (publicAccessModificator == null)
                throw new NotFoundException(VideoAccessModificators.Public, nameof(VideoAccessModificatorEntity));

            return publicAccessModificator;
        }
    }
}
