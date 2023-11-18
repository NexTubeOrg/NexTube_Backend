﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Files.Videos.Commands.RemoveVideoByEntityId;
using NexTube.Application.CQRS.Files.Videos.Commands.UploadVideo;
using NexTube.Application.CQRS.Files.Videos.Queries.GetAllVideoEntities;
using NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity;
using NexTube.Application.CQRS.Files.Videos.Queries.GetVideoUrl;
using NexTube.WebApi.DTO.Files.Video;
using WebShop.Domain.Constants;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.DeleteComment;
using NexTube.WebApi.DTO.Comments.VideoComments;

namespace NexTube.WebApi.Controllers
{
    public class VideoController : BaseController
    {
        private readonly IMapper mapper;

        public VideoController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("{videoId}")]
        public async Task<ActionResult> GetVideoUrl(string videoId)
        {
            var getVideoDto = new GetVideoUrlDto()
            {
                VideoUrl = videoId,
            };

            var query = mapper.Map<GetVideoUrlQuery>(getVideoDto);
            var getVideoUrlResult = await Mediator.Send(query);

            return Redirect(getVideoUrlResult.VideoUrl);
        }

        [HttpGet("{videoEntityId}")]
        public async Task<ActionResult> GetVideoEntity(int videoEntityId)
        {
            var getVideoEntityDto = new GetVideoEntityDto()
            {
                VideoEntityId = videoEntityId,
            };

            var query = mapper.Map<GetVideoEntityQuery>(getVideoEntityDto);
            var getVideoEntityResult = await Mediator.Send(query);

            return Ok(getVideoEntityResult);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVideoEntites()
        {
            var query = new GetAllVideoEntitiesQuery();
            var getVideoEntityResult = await Mediator.Send(query);

            return Ok(getVideoEntityResult);
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> UploadVideo([FromForm] UploadVideoDto dto)
        {
            await EnsureCurrentUserAssignedAsync();

            var command = mapper.Map<UploadVideoCommand>(dto);
            command.Creator = CurrentUser;
            var videoId = await Mediator.Send(command);

            return Ok(videoId);
        }

        [HttpDelete("{videoEntityId}")]
        public async Task<ActionResult> RemoveVideoByEntityId(int videoEntityId)
        {
            var removeVideoByEntityIdDto = new RemoveVideoByEntityIdDto()
            {
                VideoEntityId = videoEntityId,
            };

            var command = mapper.Map<RemoveVideoByEntityIdCommand>(removeVideoByEntityIdDto);
            await Mediator.Send(command);
            
            return Ok();
        }
    }
}
