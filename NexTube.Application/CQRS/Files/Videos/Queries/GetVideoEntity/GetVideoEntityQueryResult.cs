using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity
{
    public class GetVideoEntityQueryResult
    {
        public VideoEntity VideoEntity { get; set; } = null!;
    }
}
