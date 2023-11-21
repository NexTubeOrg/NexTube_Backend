using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Videos.Queries.GetAllVideos
{
    public class GetAllVideosQueryResult
    {
        public IEnumerable<VideoLookup> VideoEntities { get; set; } = null!;
    }
}
