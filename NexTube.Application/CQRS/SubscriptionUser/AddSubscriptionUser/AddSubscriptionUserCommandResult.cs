using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Identity.Users.Commands.AddSubscriptionUser{
    public class AddSubscriptionUserCommandResult
    {
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ChannelPhotoFileId { get; set; }

    }
}
