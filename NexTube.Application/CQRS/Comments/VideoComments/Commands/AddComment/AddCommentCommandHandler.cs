using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Unit>
    {
        private readonly IVideoCommentService commentService;
        public AddCommentCommandHandler(IVideoCommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            await commentService.AddCommentAsync(request.VideoId, request.Content, request.Creator);
            return Unit.Value;
        }
    }
}
