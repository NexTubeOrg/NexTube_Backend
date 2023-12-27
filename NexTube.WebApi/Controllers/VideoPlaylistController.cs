using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.ToggleVideoPlaylist;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.CreatePlaylist;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetPlaylistVideos;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetUserPlaylists;
using NexTube.WebApi.DTO.Playlists;
using WebShop.Domain.Constants;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetVideoPlaylistsUserStatus;

namespace NexTube.WebApi.Controllers {
    [Route("api/Video/Playlist/[action]")]
    public class VideoPlaylistController : BaseController {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public VideoPlaylistController(IMapper mapper, IMediator mediator) {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetUserPlaylists([FromQuery] GetUserPlaylistsDto dto) {
            var query = mapper.Map<GetUserPlaylistsQuery>(dto);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetPlaylistVideos([FromQuery] GetPlaylistVideosDto dto) {
            var query = mapper.Map<GetPlaylistVideosQuery>(dto);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> GetVideoPlaylistsUserStatus([FromQuery] GetVideoPlaylistsUserStatusDto dto) {
            var query = mapper.Map<GetVideoPlaylistsUserStatusQuery>(dto);
            query.UserId = UserId;
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> CreatePlaylist([FromForm] CreatePlaylistDto dto) {
            await EnsureCurrentUserAssignedAsync();

            var command = mapper.Map<CreatePlaylistCommand>(dto);
            command.User = CurrentUser;
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> ChangeVideoPlaylist([FromBody] ChangeVideoPlaylistDto dto) {
            var command = mapper.Map<ToggleVideoPlaylistCommand>(dto);
            command.UserId = UserId;
            await mediator.Send(command);

            return Ok();
        }
    }
}
