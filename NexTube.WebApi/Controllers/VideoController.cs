using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain.Constants;
using NexTube.Application.CQRS.Videos.Commands.UploadVideo;
using NexTube.Application.CQRS.Videos.Commands.UpdateVideo;
using NexTube.Application.CQRS.Videos.Commands.DeleteVideo;
using NexTube.WebApi.DTO.Videos;
using NexTube.Application.CQRS.Videos.Queries.GetVideoList;
using NexTube.Application.CQRS.Files.Videos.GetVideoFileUrl;
using NexTube.Application.CQRS.Videos.Queries.GetVideo;
using NexTube.Application.CQRS.Videos.Queries.GetVideoListChannel;
using NexTube.Application.CQRS.UserVideoHistories.Queries.GetUserVideoHistoryList;
using NexTube.Application.CQRS.Videos.Queries.GetRandomVideo;

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
        public async Task<ActionResult> GetVideoUrl([FromQuery] GetVideoUrlDto dto)
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
        public async Task<ActionResult> GetRandomVideo()
        {
            var command = new GetRandomVideoQuery() { };
            var getRandomVideoResult = await Mediator.Send(command);

            return Ok(getRandomVideoResult);
        }

        [HttpGet]
        public async Task<ActionResult> GetVideoList([FromQuery] GetVideoListDto dto)
        {
            var query = mapper.Map<GetVideoListQuery>(dto);            
            var getVideosDto = await Mediator.Send(query);

            return Ok(getVideosDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetVideoListChannel([FromQuery] GetVideoListChannelDto dto)
        {
            var query = mapper.Map<GetVideoListChannelQuery>(dto);
            query.RequesterId = this.UserId;

            var getVideosDto = await Mediator.Send(query);

            return Ok(getVideosDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetVideoListHistory([FromQuery] GetVideoListHistoryDto dto)
        {
            var query = mapper.Map<GetVideoListHistoryQuery>(dto);
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
            var videoLookup = await Mediator.Send(command);

            return Ok(videoLookup);
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
