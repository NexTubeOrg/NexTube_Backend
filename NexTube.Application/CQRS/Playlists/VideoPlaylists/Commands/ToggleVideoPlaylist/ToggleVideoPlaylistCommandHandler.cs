using Ardalis.GuardClauses;
using MediatR;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;
using NexTube.Domain.Entities.ManyToMany;
using WebShop.Application.Common.Exceptions;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.ToggleVideoPlaylist {
    public class ToggleVideoPlaylistCommandHandler : IRequestHandler<ToggleVideoPlaylistCommand, Unit> {
        private readonly IApplicationDbContext dbContext;
        private readonly IDateTimeService dateTimeService;

        public ToggleVideoPlaylistCommandHandler(IApplicationDbContext dbContext, IDateTimeService dateTimeService) {
            this.dbContext = dbContext;
            this.dateTimeService = dateTimeService;
        }
        public async Task<Unit> Handle(ToggleVideoPlaylistCommand request, CancellationToken cancellationToken) {
            var playlist = await dbContext.VideoPlaylists.FindAsync(request.PlaylistId);

            if (playlist is null)
                throw new NotFoundException(request.PlaylistId.ToString(), nameof(VideoPlaylistEntity));

            // prevent user to change foreign playlist
            if (request.UserId != playlist.CreatorId)
                throw new ForbiddenAccessException();

            var video = await dbContext.Videos.FindAsync(request.VideoId);
            if (video is null)
                throw new NotFoundException(request.VideoId.ToString(), nameof(VideoEntity));

            // if video is already in playlist - remove it
            // otherwise - add
            var relation = await dbContext.PlaylistsVideosManyToMany.FindAsync(video.Id, playlist.Id);
            if (relation is null)
                dbContext.PlaylistsVideosManyToMany.Add(new PlaylistsVideosManyToMany() {
                    Playlist = playlist,
                    Video = video,
                });
            else
                dbContext.PlaylistsVideosManyToMany.Remove(relation);

            video.DateModified = dateTimeService.Now;
            playlist.DateModified = dateTimeService.Now;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
