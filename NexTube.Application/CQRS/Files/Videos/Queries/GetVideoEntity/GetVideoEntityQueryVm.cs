using NexTube.Application.CQRS.Files.Videos.Queries.Common;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity
{
    public class GetVideoEntityQueryVm
    {
        public VideoLookup VideoEntity { get; set; } = null!;
    }
}
