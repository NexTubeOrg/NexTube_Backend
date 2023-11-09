﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexTube.Domain.Entities;

namespace NexTube.Persistence.Data.Configurations.Videos {
    public class VideoCommentEntityConfiguration : IEntityTypeConfiguration<VideoCommentEntity> {
        public void Configure(EntityTypeBuilder<VideoCommentEntity> builder) {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(p => p.VideoEntity)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
