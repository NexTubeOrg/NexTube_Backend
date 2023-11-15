using NexTube.Application.CQRS.Files.Videos.Queries.Common;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetAllVideoEntities
{
    public class GetAllVideoEntitiesQueryVm
    {
        public IEnumerable<VideoLookup> VideoEntities { get; set; } = null!;
    }
}
