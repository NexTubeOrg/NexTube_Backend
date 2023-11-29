using Microsoft.AspNetCore.Identity;
namespace NexTube.Domain.Entities
{
    public class ChannelEntity : IdentityUser<int> {
 
        public string? Nickname { get; set; }
        public string? Description { get; set; }
     
    }
}
