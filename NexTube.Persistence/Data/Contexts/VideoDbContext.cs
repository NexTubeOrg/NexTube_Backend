using Microsoft.EntityFrameworkCore;
using NexTube.Domain.Entities;
using NexTube.Persistence.Data.Configurations.Videos;

namespace NexTube.Persistence.Data.Contexts
{
    public class VideoDbContext : DbContext
    {
        public DbSet<VideoEntity> Videos { get; set; } = null!;

        public VideoDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VideoEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
