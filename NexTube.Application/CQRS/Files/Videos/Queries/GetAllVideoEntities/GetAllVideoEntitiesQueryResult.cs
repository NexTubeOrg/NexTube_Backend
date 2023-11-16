using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetAllVideoEntities
{
    public class GetAllVideoEntitiesQueryResult
    {
        public IEnumerable<VideoEntity> VideoEntities { get; set; } = null!;
    }
}
