
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Commands.GetChannelInfo
{
    public class GetChannelInfoCommandResult
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nickname { get; set; }
        public string? Description { get; set; }
        public int Subsciptions { get; set; }
        public int Video { get; set; }
        public Guid? ChannelPhotoFileId { get; set; }
        public Guid? BannerFileId { get; set; }
    }
}