using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
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
    }
}
