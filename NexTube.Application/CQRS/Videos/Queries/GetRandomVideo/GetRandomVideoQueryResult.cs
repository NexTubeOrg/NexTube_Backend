using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Videos.Queries.GetRandomVideo
{
    public class GetRandomVideoQueryResult
    {
        public VideoLookup Video { get; set; } = null!;
    }
}
