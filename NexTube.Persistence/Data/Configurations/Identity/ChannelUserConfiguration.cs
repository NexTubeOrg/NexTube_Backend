using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexTube.Domain.Entities;

namespace NexTube.Persistence.Data.Configurations.Identity
{
    internal class ChannelUserConfiguration : IEntityTypeConfiguration<ChannelEntity>
    {
        public void Configure(EntityTypeBuilder<ChannelEntity> builder)
        {
            #region Properties

            builder.Property(a => a.Nickname).HasMaxLength(500).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(500).IsRequired();


            #endregion
        }
    }
}