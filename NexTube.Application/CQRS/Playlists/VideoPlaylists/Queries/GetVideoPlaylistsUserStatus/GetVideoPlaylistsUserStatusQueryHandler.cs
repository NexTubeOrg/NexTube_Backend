using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetVideoPlaylistsUserStatus {
    public class GetVideoPlaylistsUserStatusQueryHandler : IRequestHandler<GetVideoPlaylistsUserStatusQuery, GetVideoPlaylistsUserStatusQueryResult> {
        private readonly IApplicationDbContext _dbContext;

        public GetVideoPlaylistsUserStatusQueryHandler(IApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<GetVideoPlaylistsUserStatusQueryResult> Handle(GetVideoPlaylistsUserStatusQuery request, CancellationToken cancellationToken) {
            var result = await _dbContext.VideoPlaylists
                .Where(v => v.CreatorId == request.UserId)
                .OrderBy(v => v.Id)
                .Select(v => new PlaylistVideoUserStatus() {
                    Playlist = new Models.Lookups.VideoPlaylistLookup() {
                        Id = v.Id,
                        Title = v.Title,
                    },
                    IsVideoInPlaylist = _dbContext.PlaylistsVideosManyToMany
                        .Any(p => p.PlaylistId == v.Id && p.VideoId == request.VideoId)
                }).ToListAsync();

            return new GetVideoPlaylistsUserStatusQueryResult() {
                Playlists = result
            };
        }
    }
}
