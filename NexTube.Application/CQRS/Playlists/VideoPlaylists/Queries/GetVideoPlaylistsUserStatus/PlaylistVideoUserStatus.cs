using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetVideoPlaylistsUserStatus {
    public class PlaylistVideoUserStatus {
        public VideoPlaylistLookup Playlist { get; set; } = null!;
        public bool IsVideoInPlaylist { get; set; }
    }
}
