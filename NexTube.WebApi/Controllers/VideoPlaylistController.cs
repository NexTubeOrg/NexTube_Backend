using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.ChangeVideoPlaylist;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.CreatePlaylist;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetUserPlaylists;
using NexTube.WebApi.DTO.Playlists;
using WebShop.Domain.Constants;

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

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> CreatePlaylist([FromForm] CreatePlaylistDto dto) {
            await EnsureCurrentUserAssignedAsync();

            var command = mapper.Map<CreatePlaylistCommand>(dto);
            command.User = CurrentUser;
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> ChangeVideoPlaylist([FromBody] ChangeVideoPlaylistDto dto) {
            var command = mapper.Map<ChangeVideoPlaylistCommand>(dto);
            command.UserId = UserId;
            await mediator.Send(command);

            return Ok();
        }
    }
}
