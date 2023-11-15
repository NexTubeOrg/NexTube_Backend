using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;

namespace NexTube.Application.CQRS.Files.Videos.Queries.Common
{
    public record VideoLookup
    {
        public int? Id { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public Guid? VideoFile { get; set; } = null;
        public Guid? PreviewPhotoFile { get; set; } = null;
        public UserLookup? Creator { get; set; } = null;
        public DateTime? DateCreated { get; set; } = null;
    }
}
