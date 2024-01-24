using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Constants;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoListChannel
{
    public class GetVideoListChannelQueryHandler : IRequestHandler<GetVideoListChannelQuery, GetVideoListChannelQueryResult>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public GetVideoListChannelQueryHandler(IApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<GetVideoListChannelQueryResult> Handle(GetVideoListChannelQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Videos
                   .Where(v => v.Creator.Id == request.ChannelId)
                   .OrderByDescending(c => c.DateCreated)
                   .Include(e => e.Creator)
                   .Skip((request.Page - 1) * request.PageSize)
                   .Take(request.PageSize)
                   .Select(v => new VideoLookup()
                   {
                       Id = v.Id,
                       Name = v.Name,
                       Description = v.Description,
                       VideoFile = v.VideoFileId,
                       AccessModificator = v.AccessModificator.Modificator,
                       PreviewPhotoFile = v.PreviewPhotoFileId,
                       DateCreated = v.DateCreated,
                       Views = v.Views,
                       CommentsCount = _dbContext.VideoComments.Count(vc => vc.VideoEntity.Id == v.Id),
                       Creator = new UserLookup()
                       {
                           UserId = v.Creator.Id,
                           FirstName = v.Creator.FirstName,
                           LastName = v.Creator.LastName,
                           ChannelPhoto = v.Creator.ChannelPhotoFileId.ToString(),
                       }
                   });

            if (request.ChannelId != request.RequesterId)
            {
                query = query.Where(v => v.AccessModificator == VideoAccessModificators.Public);
            }

            var videoLookups = await query.ToListAsync();

            var result = new GetVideoListChannelQueryResult()
            {
                Videos = videoLookups
            };

            return result;
        }
    }
}
