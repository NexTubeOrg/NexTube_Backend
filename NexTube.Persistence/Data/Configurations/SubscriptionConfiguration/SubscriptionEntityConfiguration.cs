using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexTube.Domain.Entities;

namespace NexTube.Persistence.Data.Configurations.Identity
{
    public class SubscriptionEntityConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
    {
        public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}