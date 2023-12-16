using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetPlaylistVideos {
    public class GetPlaylistVideosQueryResult {
        public IList<VideoLookup> Videos { get; set; } = null!;
    }
}
