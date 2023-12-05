using MediatR;
using NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoCountReactions;

namespace NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoCountReactionsWithUserReaction {
    public class GetVideoCountReactionsWithUserReactionQuery : IRequest<VideoReactionsCountWithStatus> {
        public int VideoId { get; set; }
        public int? RequesterId { get; set; } = null;
    }
}
