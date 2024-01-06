using MediatR;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Videos.Commands.UploadVideo
{
    public class UploadVideoCommand : IRequest<VideoLookup>
    {
        public Stream Source { get; set; } = null!;
        public Stream PreviewPhotoSource { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ApplicationUser? Creator { get; set; }
        public string? AccessModificator { get; set; } = null;
    }
}
