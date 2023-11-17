using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity
{
    public class GetVideoEntityQueryResult
    {
        public VideoLookup VideoEntity { get; set; } = null!;
    }
}
