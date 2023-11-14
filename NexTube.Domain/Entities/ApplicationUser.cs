using Microsoft.AspNetCore.Identity;

namespace NexTube.Domain.Entities {
    public class ApplicationUser : IdentityUser<int> {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
