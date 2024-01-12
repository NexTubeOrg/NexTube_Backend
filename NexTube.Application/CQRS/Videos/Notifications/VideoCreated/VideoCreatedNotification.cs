using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Videos.Notifications.VideoCreated {
    public class VideoCreatedNotification : INotification {
        public VideoEntity Video { get; set; } = null!;
    }
}
