using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain.Constants;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.DeleteComment;
using NexTube.WebApi.DTO.Comments.VideoComments;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddCommentReply;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentRepliesList;
using NexTube.Application.CQRS.Reactions.VideoReactions.Commands.SetReaction;
using NexTube.Domain.Entities;
using NexTube.WebApi.DTO.Reactions.VideoReactions;

namespace NexTube.WebApi.Controllers {
    [Route("api/Video/React/[action]")]
    public class VideoReactionController : BaseController {
        private readonly IMapper mapper;

        public VideoReactionController(IMapper mapper) {
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> ReactVideo([FromBody] ToggleVideoReactionDto dto) {
            await EnsureCurrentUserAssignedAsync();

            var command = mapper.Map<SetReactionCommand>(dto);
            command.Requester = CurrentUser;

            var result = await Mediator.Send(command);

            return Ok(result);
        }
    }
}
