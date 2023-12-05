using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexTube.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Persistence.Data.Configurations.Videos
{
    public class VideoAccessModificatorEntityConfiguration : IEntityTypeConfiguration<VideoAccessModificatorEntity>
    {
        public void Configure(EntityTypeBuilder<VideoAccessModificatorEntity> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
