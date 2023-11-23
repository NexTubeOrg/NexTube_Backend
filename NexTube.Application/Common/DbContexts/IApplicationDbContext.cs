﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NexTube.Domain.Entities;

namespace NexTube.Application.Common.DbContexts {
    public interface IApplicationDbContext {
        DbSet<VideoEntity> Videos { get; set; }
        DbSet<VideoCommentEntity> VideoComments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
