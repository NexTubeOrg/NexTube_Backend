using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Constants;
using System.Linq;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetPlaylistVideos {
    public class GetPlaylistVideosQueryHandler : IRequestHandler<GetPlaylistVideosQuery, GetPlaylistVideosQueryResult> {
        private readonly IApplicationDbContext _dbContext;

        public GetPlaylistVideosQueryHandler(IApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<GetPlaylistVideosQueryResult> Handle(GetPlaylistVideosQuery request, CancellationToken cancellationToken) {
            var playlist = await _dbContext.VideoPlaylists.FindAsync(request.PlaylistId);

            var videos = await _dbContext.PlaylistsVideosManyToMany
                .Where(pv => pv.PlaylistId == request.PlaylistId)
                .Include(pv => pv.Video)
                .ThenInclude(v => v.Creator)
                .OrderByDescending(pv => pv.Video.DateCreated)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(v => new VideoLookup() {
                    Id = v.Video.Id,
                    Name = v.Video.Name,
                    PreviewPhotoFile = v.Video.PreviewPhotoFileId,
                    Creator = new UserLookup() {
                        UserId = v.Video.CreatorId,
                        FirstName = v.Video.Creator!.FirstName,
                        LastName = v.Video.Creator.LastName,
                        ChannelPhoto = v.Video.Creator.ChannelPhotoFileId.ToString()
                    }
                })
               .ToListAsync();

            var result = new GetPlaylistVideosQueryResult() {
                Title = playlist.Title,
                Videos = videos,
                TotalCount = await _dbContext.PlaylistsVideosManyToMany
                    .Where(pv => pv.PlaylistId == request.PlaylistId)
                    .CountAsync()
            };

            return result;
        }
    }
}
