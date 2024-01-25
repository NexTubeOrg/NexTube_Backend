using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoListHistory
{
    public class GetVideoListHistoryQueryResult
    {
        public IEnumerable<VideoLookup> Videos { get; set; }
    }
}
