using MediatR;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.ToggleVideoPlaylist {
    public class ToggleVideoPlaylistCommand : IRequest<Unit> {
        public int VideoId { get; set; }
        public int PlaylistId { get; set; }
        public int UserId { get; set; }
    }
}
