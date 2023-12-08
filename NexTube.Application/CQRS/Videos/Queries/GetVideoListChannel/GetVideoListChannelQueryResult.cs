using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoListChannel
{
    public class GetVideoListChannelQueryResult
    {
        public IEnumerable<VideoLookup> Videos { get; set; } = null;
    }
}
