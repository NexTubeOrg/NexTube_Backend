using MediatR;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment {
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, CommentLookup> {
        private readonly IVideoCommentService commentService;
        public AddCommentCommandHandler(IVideoCommentService commentService) {
            this.commentService = commentService;
        }

        public async Task<CommentLookup> Handle(AddCommentCommand request, CancellationToken cancellationToken) {
            var res = await commentService.AddCommentAsync(request.VideoId, request.Content, request.Creator);
            return res.Comment;
        }
    }
}
