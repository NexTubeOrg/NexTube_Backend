using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain.Constants;
using NexTube.Application.CQRS.Videos.Commands.UploadVideo;
using NexTube.Application.CQRS.Files.Videos.GetVideoFileUrl;
using NexTube.Application.CQRS.Videos.Commands.UpdateVideo;
using NexTube.Application.CQRS.Videos.Commands.DeleteVideo;
using NexTube.WebApi.DTO.Videos;
using NexTube.Application.CQRS.Videos.Queries.GetVideoList;
using NexTube.Application.CQRS.Videos.Queries.GetVideo;

namespace NexTube.WebApi.Controllers
{
    public class VideoController : BaseController
    {
        private readonly IMapper mapper;

        public VideoController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetVideoUrl([FromQuery] GetVideoFileUrlDto dto)
        {
            var query = mapper.Map<GetVideoUrlQuery>(dto);
            var getVideoUrlResult = await Mediator.Send(query);

            return Redirect(getVideoUrlResult.VideoUrl);
        }

        [HttpGet]
        public async Task<ActionResult> GetVideo([FromQuery] GetVideoDto dto)
        {
            var query = mapper.Map<GetVideoQuery>(dto);
            query.RequesterId = this.UserId;

            var getVideoByIdResult = await Mediator.Send(query);

            return Ok(getVideoByIdResult);
        }

        [HttpGet]
        public async Task<ActionResult> GetVideoList([FromQuery] GetVideoListDto dto)
        {
            var query = mapper.Map<GetVideoListQuery>(dto);
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
