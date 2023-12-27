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
            //var query1 = _dbContext.PlaylistsVideos
            //    .Where(pv => pv.VideoId == request.VideoId)
            //    .Join(_dbContext.VideoPlaylists, arg => arg.PlaylistId,
            //        arg => arg.Id,
            //        (pv, playlist) => playlist)
            //    .Where(p => p.CreatorId == request.UserId)
            //    .Select(p => new {
            //        Playlist = new VideoPlaylistLookup() {
            //            Id = p.Id,
            //            Preview = p.PreviewImage.ToString(),
            //            Title = p.Title,
            //        },
            //        IsInPlaylist = true
            //    });

            //var query2 = _dbContext.VideoPlaylists
            //    .Where(vp => vp.CreatorId == request.UserId)
            //    .Select(p => new {
            //        Playlist = new VideoPlaylistLookup() {
            //            Id = p.Id,
            //            Preview = p.PreviewImage.ToString(),
            //            Title = p.Title,
            //        },
            //        IsInPlaylist = false
            //    });

            //var union = query1
            //    .Union(query2)
            //    .Select(p => new Tuple<VideoPlaylistLookup, bool>(p.Playlist, p.IsInPlaylist));

            var result = await _dbContext.VideoPlaylists
                .Where(v => v.CreatorId == request.UserId)
                .OrderBy(v => v.Id)
                .Select(v => new PlaylistVideoUserStatus() {
                    Playlist = new Models.Lookups.VideoPlaylistLookup() {
                        Id = v.Id,
                        Title = v.Title,
                    },
                    IsVideoInPlaylist = _dbContext.PlaylistsVideos
                        .Any(p => p.PlaylistId == v.Id && p.VideoId == request.VideoId)
                }).ToListAsync();

            return new GetVideoPlaylistsUserStatusQueryResult() {
                Playlists = result
            };
        }
    }
}
