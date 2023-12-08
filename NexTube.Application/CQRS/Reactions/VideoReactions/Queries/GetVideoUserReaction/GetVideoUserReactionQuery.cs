using MediatR;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoUserReaction {
    public class GetVideoUserReactionQuery : IRequest<ReactionLookup?> {
        public int UserId { get; set; }
        public int VideoId { get; set; }
    }
}
