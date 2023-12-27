namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetVideoPlaylistsUserStatus {
    public class GetVideoPlaylistsUserStatusQueryResult {
        public List<PlaylistVideoUserStatus> Playlists { get; set; } = null!;
    }
}
