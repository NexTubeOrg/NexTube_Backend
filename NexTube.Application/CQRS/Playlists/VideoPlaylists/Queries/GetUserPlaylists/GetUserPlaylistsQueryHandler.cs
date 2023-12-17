using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetUserPlaylists {
    public class GetUserPlaylistsQueryHandler : IRequestHandler<GetUserPlaylistsQuery, GetUserPlaylistsQueryResult> {
        private readonly IApplicationDbContext _dbContext;

        public GetUserPlaylistsQueryHandler(IApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<GetUserPlaylistsQueryResult> Handle(GetUserPlaylistsQuery request, CancellationToken cancellationToken) {
            var query = _dbContext.VideoPlaylists
                .Where(p => p.Creator.Id == request.UserId)
                .OrderByDescending(c => c.DateModified)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new VideoPlaylistLookup() {
                    Id = p.Id,
                    Title = p.Title,
                    TotalCountVideos = p.PlaylistsVideos.Count(),
                    Preview = p.PreviewImage.ToString()
                });

            var result = await query.ToListAsync();
            return new GetUserPlaylistsQueryResult() {
                Playlists = result
            };
        }
    }
}
