namespace NexTube.Domain.Entities
{
    public class VideoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid? VideoId { get; set; } = null!;
        public Guid? PreviewPhotoId { get; set; } = null!;
    }
}
