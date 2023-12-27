using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Identity.Users.Commands.DeleteSubscriptionUserCommand
{
    public class DeleteSubscriptionUserCommandResult
    {
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ChannelPhotoFileId { get; set; }

    }
}
