using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoById
{
    public class GetVideoByIdQueryResult
    {
        public VideoLookup Video { get; set; } = null!;
    }
}
