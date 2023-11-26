using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities
{
    public class VideoEntity : OwnedEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid? VideoFileId { get; set; } = null!;
        public Guid? PreviewPhotoFileId { get; set; } = null!;

        public VideoAccessModificatorEntity? AccessModificator { get; set; } = null!;
    }
}
