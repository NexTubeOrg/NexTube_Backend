using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity
{
    public class GetVideoEntityQueryVm
    {
        public VideoEntity VideoEntity { get; set; } = null!;
    }
}
