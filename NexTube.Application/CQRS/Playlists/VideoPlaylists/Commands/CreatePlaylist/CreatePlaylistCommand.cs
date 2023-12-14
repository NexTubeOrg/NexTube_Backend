using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.CreatePlaylist {
    public class CreatePlaylistCommand : IRequest<Unit> {
        public string Title { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public Stream? PreviewImageStream { get; set; }
    }
}
