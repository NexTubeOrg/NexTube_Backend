using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Files.Videos.GetVideoFileUrl
{
    public class GetVideoUrlQueryHandler : IRequestHandler<GetVideoUrlQuery, GetVideoUrlQueryResult>
    {
        private readonly IVideoService _videoService;
        private readonly IApplicationDbContext _dbContext;

        public GetVideoUrlQueryHandler(IVideoService videoService, IApplicationDbContext dbContext)
        {
            _videoService = videoService;
            _dbContext = dbContext;
        }

        public async Task<GetVideoUrlQueryResult> Handle(GetVideoUrlQuery request, CancellationToken cancellationToken)
        {
            var result = await _videoService.GetUrlVideoAsync(request.VideoFileId);

            var video = await _dbContext.Videos.Where(v => v.VideoFileId.ToString() == request.VideoFileId).FirstOrDefaultAsync();

            if (video == null)
            {
                throw new NotFoundException(request.VideoFileId, nameof(VideoEntity));
            }

            video.Views += 1;

            await _dbContext.SaveChangesAsync(cancellationToken);

            var getVideoUrlResult = new GetVideoUrlQueryResult()
            {
                VideoUrl = result.VideoUrl,
            };

            return getVideoUrlResult;
        }
    }
}
