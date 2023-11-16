using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList
{
    public class GetCommentsListQueryHandler : IRequestHandler<GetCommentsListQuery, GetCommentsListQueryResult>
    {
        private readonly IVideoCommentService commentService;
        public GetCommentsListQueryHandler(IVideoCommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task<GetCommentsListQueryResult> Handle(GetCommentsListQuery request, CancellationToken cancellationToken)
        {
            var responce = await commentService.GetCommentsListAsync(request.VideoId, request.Page, request.PageSize);

            return new GetCommentsListQueryResult()
            {
                Comments = responce.Comments,
            };
        }
    }
}
