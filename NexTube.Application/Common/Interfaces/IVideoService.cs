﻿using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList;
using NexTube.Domain.Entities;

namespace NexTube.Application.Common.Interfaces
{
    public interface IVideoService
    {
        Task<(Result Result, int VideoEntityId)> UploadVideo(string name, string description, Stream previewPhotoSource, Stream source, ApplicationUser creator);
        Task<(Result Result, string VideoUrl)> GetUrlVideo(string videoId);
        Task<(Result Result, VideoEntity VideoEntity)> GetVideoEntity(int videoEntityId);
        Task<(Result Result, IEnumerable<VideoEntity> VideoEntities)> GetAllVideoEntities();
        Task<Result> RemoveVideoByEntityId(int videoEntityId);
    }
}
