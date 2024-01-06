using MediatR;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetVideoPlaylistsUserStatus {
    public class GetVideoPlaylistsUserStatusQuery : IRequest<GetVideoPlaylistsUserStatusQueryResult> {
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
