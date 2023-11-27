using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.Identity.Users.Commands.SubscriptionsUser;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;
using NexTube.Persistence.Data.Configurations.Comments.VideoComments;
using NexTube.Persistence.Data.Configurations.Identity;
using NexTube.Persistence.Data.Configurations.Videos;
using System.Reflection.Emit;

namespace NexTube.Persistence.Data.Contexts {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>, IApplicationDbContext {
        public DbSet<VideoEntity> Videos { get; set; } = null!;
        public DbSet<VideoCommentEntity> VideoComments { get; set; } = null!;
        public DbSet<Subscription > Subscriptions { get; set; } = null!;
        public DbSet<UserLookup> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.Entity<UserLookup>().HasKey(x => x.UserId);
            builder.Entity<Subscription>().HasKey(x => x.Id);
            builder.ApplyConfiguration(new VideoEntityConfiguration());
            builder.ApplyConfiguration(new VideoCommentEntityConfiguration());

            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            // default behaviour
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
