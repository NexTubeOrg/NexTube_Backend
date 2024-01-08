using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace NexTube.Domain.Entities {
    public class ApplicationUser : IdentityUser<int> {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nickname { get; set; }
        public string? Description { get; set; }
        public Guid? ChannelPhotoFileId { get; set; }
        public Guid? BannerFileId { get; set; }
    }
}
