using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.DeleteComment {
    public class DeleteCommentCommand : IRequest<Unit> {
        public int CommentId { get; set; }
        public ApplicationUser? Requester { get; set; } = null;
    }
}
