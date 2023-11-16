using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexTube.Domain.Entities;

namespace NexTube.Persistence.Data.Configurations.Identity
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            #region Properties

            builder.Property(a => a.FirstName).HasMaxLength(500).IsRequired();
            builder.Property(a => a.LastName).HasMaxLength(500).IsRequired();


            #endregion
        }
    }
}
