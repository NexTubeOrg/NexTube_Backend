using MediatR;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Videos.Commands.UpdateVideo
{
    public class UpdateVideoCommand : IRequest<VideoLookup>
    {
        public int? VideoId { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string? AccessModificator { get; set; } = null!;
        public int? RequesterId { get; set; } = null!;
    }
}
