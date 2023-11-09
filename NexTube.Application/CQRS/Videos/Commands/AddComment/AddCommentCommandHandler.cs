using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Videos.Commands.AddComment {
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Unit> {
        private readonly IVideoService videoService;
        public AddCommentCommandHandler(IVideoService videoService) {
            this.videoService = videoService;
        }

        public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken) {
            await videoService.AddCommentAsync(request.VideoId, request.AuthorUserId, request.Content);
            return Unit.Value;
        }
    }
}
