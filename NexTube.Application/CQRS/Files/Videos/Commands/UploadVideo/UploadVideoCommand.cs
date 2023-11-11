using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Files.Videos.Commands.UploadVideo
{
    public class UploadVideoCommand : IRequest<int>
    {
        public Stream Source { get; set; } = null!;
        public Stream PreviewPhotoSource { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ApplicationUser? Creator { get; set; }
    }
}
