namespace NexTube.Domain.Entities.ManyToMany {
    public class PlaylistsVideosManyToMany {
        public int VideoId { get; set; }
        public VideoEntity Video { get; set; } = null!;

        public int PlaylistId { get; set; }
        public VideoPlaylistEntity Playlist { get; set; } = null!;
    }
}
