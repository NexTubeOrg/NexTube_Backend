﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain.Constants;
using NexTube.Application.CQRS.Videos.Commands.UploadVideo;
using NexTube.Application.CQRS.Videos.Queries.GetVideoById;
using NexTube.Application.CQRS.Files.Videos.GetVideoFileUrl;
using NexTube.Application.CQRS.Videos.Queries.GetAllVideos;
using NexTube.Application.CQRS.Videos.Commands.UpdateVideo;
using NexTube.Application.CQRS.Videos.Commands.DeleteVideo;
using NexTube.WebApi.DTO.Videos;

namespace NexTube.WebApi.Controllers
{
    public class VideoController : BaseController
    {
        private readonly IMapper mapper;

        public VideoController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("{videoFileId}")]
        public async Task<ActionResult> GetVideoFileUrl(string videoFileId)
        {
            var getVideoDto = new GetVideoFileUrlDto()
            {
                VideoFileId = videoFileId,
            };

            var query = mapper.Map<GetVideoUrlQuery>(getVideoDto);
            var getVideoUrlResult = await Mediator.Send(query);

            return Redirect(getVideoUrlResult.VideoUrl);
        }

        [HttpGet]
        public async Task<ActionResult> GetVideo([FromQuery] GetVideoDto dto)
        {
            var query = mapper.Map<GetVideoByIdQuery>(dto);
            query.RequesterId = this.UserId;

            var getVideoByIdResult = await Mediator.Send(query);

            return Ok(getVideoByIdResult);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVideos([FromQuery] GetVideoListDto dto)
        {
            var query = mapper.Map<GetAllVideosQuery>(dto);
            query.RequesterId = this.UserId;
            
            var getVideosDto = await Mediator.Send(query);

            return Ok(getVideosDto);
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

        [HttpDelete]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> DeleteVideo([FromQuery] DeleteVideoDto dto)
        {
            var command = mapper.Map<DeleteVideoCommand>(dto);
            command.RequesterId = this.UserId;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> UpdateVideo([FromForm] UpdateVideoDto dto)
        {
            var command = mapper.Map<UpdateVideoCommand>(dto);
            command.RequesterId = this.UserId;
            var videoLookup = await Mediator.Send(command);

            return Ok(videoLookup);
        }
    }
}
