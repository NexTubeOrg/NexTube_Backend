using MediatR;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
    {
        private readonly IVideoCommentService commentService;
        public DeleteCommentCommandHandler(IVideoCommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            await commentService.DeleteCommentAsync(request.CommentId);
            return Unit.Value;
        }
    }
}
