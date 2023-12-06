using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetUserPlaylists;
using NexTube.WebApi.DTO.Playlists;

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
    }
}
