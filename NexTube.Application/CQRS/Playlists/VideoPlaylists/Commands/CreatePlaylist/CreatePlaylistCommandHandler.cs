using MediatR;
using NexTube.Application.Common.DbContexts;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.CreatePlaylist {
    public class CreatePlaylistCommandHandler : IRequestHandler<CreatePlaylistCommand, Unit> {
        private readonly IApplicationDbContext dbContext;
        private readonly IDateTimeService dateTimeService;

        public CreatePlaylistCommandHandler(IApplicationDbContext dbContext, IDateTimeService dateTimeService) {
            this.dbContext = dbContext;
            this.dateTimeService = dateTimeService;
        }

        public async Task<Unit> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken) {
            var playlist = new VideoPlaylistEntity() {
                Creator = request.User,
                Title = request.Title,
                DateCreated = dateTimeService.Now,
            };

            dbContext.VideoPlaylists.Add(playlist);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
