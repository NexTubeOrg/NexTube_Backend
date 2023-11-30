using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoCountReactions;
using NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoUserReaction;

namespace NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoCountReactionsWithUserReaction {
    public class GetVideoCountReactionsWithUserReactionQueryHandler : IRequestHandler<GetVideoCountReactionsWithUserReactionQuery, VideoReactionsCountWithStatus> {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public GetVideoCountReactionsWithUserReactionQueryHandler(IApplicationDbContext dbContext, IMediator mediator) {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<VideoReactionsCountWithStatus> Handle(GetVideoCountReactionsWithUserReactionQuery request, CancellationToken cancellationToken) {
            var result = new VideoReactionsCountWithStatus();

            var videoReactionsCount = await _mediator.Send(new GetVideoCountReactionsQuery() {
                VideoId = request.VideoId,
            });
            result.Likes = videoReactionsCount.Likes;
            result.Dislikes = videoReactionsCount.Dislikes;

            // get current user reaction status
            if (request.RequesterId is not null) {
                var getUserReactionQuery = new GetVideoUserReactionQuery() {
                    UserId = request.RequesterId.Value,
                    VideoId = request.VideoId
                };

                try {
                    var reactions = await _mediator.Send(getUserReactionQuery);
                    result.WasLikedByRequester = reactions?.ReactionType == Domain.Entities.VideoReactionEntity.VideoReactionType.Like;
                    result.WasDislikedByRequester = reactions?.ReactionType == Domain.Entities.VideoReactionEntity.VideoReactionType.Dislike;
                }
                catch (NotFoundException) {
                    // user did not reacted to the video
                }
            }

            return result;
        }
    }
}
