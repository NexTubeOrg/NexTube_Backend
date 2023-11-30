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
using NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoUserReaction;
using NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoCountReactions;
using NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoCountReactionsWithUserReaction;

namespace NexTube.WebApi.Controllers {
    [Route("api/Video/Reaction/[action]")]
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

        [HttpGet("{videoId}")]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> GetRequesterReaction([FromRoute] int videoId) {
            var query = new GetVideoUserReactionQuery() {
                UserId = this.UserId,
                VideoId = videoId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{videoId}")]
        public async Task<ActionResult> GetCountVideoReactions([FromRoute] int videoId) {
            var query = new GetVideoCountReactionsQuery() {
                VideoId = videoId,
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{videoId}")]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult> GetCountVideoReactionsWithStatus([FromRoute] int videoId) {
            var query = new GetVideoCountReactionsWithUserReactionQuery() {
                VideoId = videoId,
                RequesterId = UserId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
