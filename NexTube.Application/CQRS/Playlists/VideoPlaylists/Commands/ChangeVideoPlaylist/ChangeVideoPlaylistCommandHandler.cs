using Ardalis.GuardClauses;
using MediatR;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;
using WebShop.Application.Common.Exceptions;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.ChangeVideoPlaylist {
    public class ChangeVideoPlaylistCommandHandler : IRequestHandler<ChangeVideoPlaylistCommand, Unit> {
        private readonly IApplicationDbContext dbContext;
        private readonly IDateTimeService dateTimeService;

        public ChangeVideoPlaylistCommandHandler(IApplicationDbContext dbContext, IDateTimeService dateTimeService) {
            this.dbContext = dbContext;
            this.dateTimeService = dateTimeService;
        }
        public async Task<Unit> Handle(ChangeVideoPlaylistCommand request, CancellationToken cancellationToken) {
            var playlist = await dbContext.VideoPlaylists.FindAsync(request.PlaylistId);

            if (playlist is null)
                throw new NotFoundException(request.PlaylistId.ToString(), nameof(VideoPlaylistEntity));

            // prevent user to change foreign playlist
            if (request.UserId != playlist.CreatorId)
                throw new ForbiddenAccessException();

            var video = await dbContext.Videos.FindAsync(request.VideoId);
            if (video is null)
                throw new NotFoundException(request.VideoId.ToString(), nameof(VideoEntity));

            // prevent user to change foreign video
            if (request.UserId != video.CreatorId)
                throw new ForbiddenAccessException();

            video.Playlist = playlist;
            video.DateModified = dateTimeService.Now;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
