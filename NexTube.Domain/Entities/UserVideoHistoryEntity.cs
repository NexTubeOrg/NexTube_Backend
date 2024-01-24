
namespace NexTube.Domain.Entities
{
    public class UserVideoHistoryEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public int VideoId { get; set; }
        public VideoEntity Video { get; set; } = null!;
        
        public DateTime DateWatched { get; set; }
    }
}
