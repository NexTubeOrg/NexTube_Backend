using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetUserPlaylists {
    public class GetUserPlaylistsQueryResult {
        public IList<VideoPlaylistLookup> Playlists { get; set; } = null!;
    }
}
