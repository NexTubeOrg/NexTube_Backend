using MediatR;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Videos.Commands.AddComment;

namespace NexTube.Application.CQRS.Videos.Commands.DeleteComment {
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit> {
        private readonly IVideoService videoService;
        public DeleteCommentCommandHandler(IVideoService videoService) {
            this.videoService = videoService;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken) {
            await videoService.DeleteCommentAsync(request.CommentId);
            return Unit.Value;
        }
    }
}
