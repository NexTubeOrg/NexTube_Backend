using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Reactions.VideoReactions.Commands.SetReaction {
    public class SetReactionCommandHandler : IRequestHandler<SetReactionCommand, Unit> {
        private readonly IDateTimeService _dateTimeService;
        private readonly IApplicationDbContext _dbContext;
        public SetReactionCommandHandler(IDateTimeService dateTimeService, IApplicationDbContext dbContext) {
            _dateTimeService = dateTimeService;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(SetReactionCommand request, CancellationToken cancellationToken) {
            var video = await _dbContext.Videos.FindAsync(request.ReactedVideoId);

            if (video is null)
                throw new NotFoundException(request.ReactedVideoId.ToString(), nameof(VideoEntity));

            var existingReaction = await _dbContext.VideoReactions
                    .Where((r) =>
                        r.ReactedVideo == video &&
                        r.Creator == request.Requester)
                    .SingleOrDefaultAsync();

            if (existingReaction is not null) {
                if (existingReaction.Type == request.ReactionType) {
                    // just reset reaction
                    _dbContext.VideoReactions.Remove(existingReaction);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                else {
                    // set new reaction type
                    existingReaction.Type = request.ReactionType;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
            }

            var reaction = new VideoReactionEntity() {
                Type = request.ReactionType,
                ReactedVideo = video,
                Creator = request.Requester,
                DateCreated = _dateTimeService.Now
            };
            _dbContext.VideoReactions.Add(reaction);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
