using MediatR;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.CreatePlaylist {
    public class CreatePlaylistCommand : IRequest<VideoPlaylistLookup> {
        public string Title { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public Stream? PreviewImageStream { get; set; }
    }
}
