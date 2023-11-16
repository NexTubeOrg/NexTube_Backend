using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexTube.Domain.Entities;
using NexTube.Persistence.Data.Configurations.Comments.VideoComments;
using NexTube.Persistence.Data.Configurations.Identity;
using NexTube.Persistence.Data.Configurations.Videos;

namespace NexTube.Persistence.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<VideoEntity> Videos { get; set; } = null!;
        public DbSet<VideoCommentEntity> VideoComments { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());

            builder.ApplyConfiguration(new VideoEntityConfiguration());
            builder.ApplyConfiguration(new VideoCommentEntityConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
