using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities {
    public class NotificationEntity : AuditableEntity {
        public enum NotificationType {
            NewVideo
        }
        public NotificationType Type { get; set; }
        public ApplicationUser? NotificationIssuer { get; set; }
        public int? NotificationIssuerId { get; set; }
        public VideoEntity? NotificationData { get; set; } = null;
    }
}
