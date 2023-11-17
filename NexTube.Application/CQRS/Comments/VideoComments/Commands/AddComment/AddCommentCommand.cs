using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment
{
    public class AddCommentCommand : IRequest<Unit>
    {
        public string Content { get; set; } = string.Empty;
        public int? VideoId { get; set; } = null;
        public ApplicationUser? Creator { get; set; } = null;
    }
}
