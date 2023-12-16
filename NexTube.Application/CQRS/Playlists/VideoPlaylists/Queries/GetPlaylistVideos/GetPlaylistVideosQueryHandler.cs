using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Constants;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetPlaylistVideos {
    public class GetPlaylistVideosQueryHandler : IRequestHandler<GetPlaylistVideosQuery, GetPlaylistVideosQueryResult> {
        private readonly IApplicationDbContext _dbContext;

        public GetPlaylistVideosQueryHandler(IApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<GetPlaylistVideosQueryResult> Handle(GetPlaylistVideosQuery request, CancellationToken cancellationToken) {
            var videos = await _dbContext.Videos
               .Where(v =>
                    v.AccessModificator.Modificator == VideoAccessModificators.Public &&
                    v.PlaylistId == request.PlaylistId)
               .OrderByDescending(c => c.DateCreated)
               .Include(e => e.Creator)
               .Skip((request.Page - 1) * request.PageSize)
               .Take(request.PageSize)
               .Select(v => new VideoLookup() {
                   Id = v.Id,
                   Name = v.Name,
                   PreviewPhotoFile = v.PreviewPhotoFileId,
                   Creator = new UserLookup() {
                       UserId = v.CreatorId,
                       FirstName = v.Creator.FirstName,
                       LastName = v.Creator.LastName,
                   }
               })
               .ToListAsync();

            var result = new GetPlaylistVideosQueryResult() {
                Videos = videos,
            };

            return result;
        }
    }
}
