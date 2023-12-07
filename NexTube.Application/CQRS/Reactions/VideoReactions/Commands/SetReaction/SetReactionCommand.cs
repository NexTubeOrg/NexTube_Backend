using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Reactions.VideoReactions.Commands.SetReaction {
    public class SetReactionCommand : IRequest<Unit> {
        public ApplicationUser Requester { get; set; } = null!;
        public int? ReactedVideoId { get; set; } = null!;
        public VideoReactionEntity.VideoReactionType ReactionType { get; set; }
    }
}
