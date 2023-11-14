using MediatR;

namespace NexTube.Application.CQRS.Videos.Commands.DeleteComment {
    public class DeleteCommentCommand : IRequest<Unit> {
        public int CommentId { get; set; }
    }
}
