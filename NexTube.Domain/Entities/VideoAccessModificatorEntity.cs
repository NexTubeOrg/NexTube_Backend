using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities
{
    public class VideoAccessModificatorEntity : BaseEntity
    {
        public string Modificator { get; set; } = string.Empty;
    }
}
