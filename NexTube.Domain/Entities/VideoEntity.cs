using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities
{
    public class VideoEntity : OwnedEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid? VideoId { get; set; } = null!;
        public Guid? PreviewPhotoId { get; set; } = null!;
    }
}
