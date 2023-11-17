using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetAllVideoEntities
{
    public class GetAllVideoEntitiesQueryResult
    {
        public IEnumerable<VideoLookup> VideoEntities { get; set; } = null!;
    }
}
