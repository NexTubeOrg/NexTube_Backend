using Microsoft.AspNetCore.Identity;

namespace NexTube.Persistence.Identity {
    public class ApplicationUser : IdentityUser<int> {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
