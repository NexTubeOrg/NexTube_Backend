using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetAllVideoEntities
{
    public class GetAllVideoEntitiesQueryVm
    {
        public IEnumerable<VideoEntity> VideoEntities { get; set; } = null!;
    }
}
