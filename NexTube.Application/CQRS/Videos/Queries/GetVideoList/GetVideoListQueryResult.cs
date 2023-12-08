using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoList
{
    public class GetVideoListQueryResult
    {
        public IEnumerable<VideoLookup> Videos { get; set; } = null!;
    }
}
