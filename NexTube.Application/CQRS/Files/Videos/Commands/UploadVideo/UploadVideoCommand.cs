using MediatR;

namespace NexTube.Application.CQRS.Files.Videos.Commands.UploadVideo
{
    public class UploadVideoCommand : IRequest<string>
    {
        public Stream Source { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
