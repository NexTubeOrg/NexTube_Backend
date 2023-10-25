using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexTube.Persistence.Data.Configurations.Identity;
using NexTube.Persistence.Identity;

namespace NexTube.Persistence.Data.Contexts
{
    public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
