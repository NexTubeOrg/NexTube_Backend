using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideo
{
    public class GetVideoQueryResult
    {
        public VideoLookup Video { get; set; } = null!;
    }
}
