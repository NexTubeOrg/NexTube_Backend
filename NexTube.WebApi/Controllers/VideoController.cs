using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexTube.WebApi.DTO.Files.Video;
using WebShop.Domain.Constants;
using NexTube.Application.CQRS.Videos.Commands.UploadVideo;
using NexTube.Application.CQRS.Videos.Commands.DeleteVideoById;
using NexTube.Application.CQRS.Videos.Queries.GetVideoById;
using NexTube.Application.CQRS.Files.Videos.GetVideoFileUrl;
using NexTube.Application.CQRS.Videos.Queries.GetAllVideos;
using NexTube.Application.CQRS.Videos.Commands.UpdateVideo;

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

        [HttpGet("{videoId}")]
        public async Task<ActionResult> GetVideo(int videoId)
        {
            var user = HttpContext.User.FindFirst("userId");

            var getVideoDto = new GetVideoDto()
            {
                VideoId = videoId,
                UserId = UserId
            };

            var query = mapper.Map<GetVideoByIdQuery>(getVideoDto);
            var getVideoByIdResult = await Mediator.Send(query);

            return Ok(getVideoByIdResult);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllVideos()
        {

            var query = new GetAllVideosQuery()
            {
                UserId = this.UserId
            };
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

        [HttpDelete("{videoId}")]
        public async Task<ActionResult> DeleteVideoById(int videoId)
        {
            var deleteVideoByIdDto = new DeleteVideoById()
            {
                VideoId = videoId,
            };

            var command = mapper.Map<DeleteVideoByIdCommand>(deleteVideoByIdDto);
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateVideo([FromForm] UpdateVideoDto dto)
        {
            var command = mapper.Map<UpdateVideoCommand>(dto);
            var videoLookup = await Mediator.Send(command);

            return Ok(videoLookup);
        }
    }
}
