using MediatR;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<Unit>
    {
        public int CommentId { get; set; }
    }
}
