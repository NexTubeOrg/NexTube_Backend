using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Videos.Queries.GetCommentsList {
    public class GetCommentsListQueryHandler : IRequestHandler<GetCommentsListQuery, GetCommentsListQueryVm> {
        private readonly IVideoService videoService;
        public GetCommentsListQueryHandler(IVideoService videoService) {
            this.videoService = videoService;
        }

        public async Task<GetCommentsListQueryVm> Handle(GetCommentsListQuery request, CancellationToken cancellationToken) {
            var responce = await videoService.GetCommentsListAsync(request.VideoId);
           
            return new GetCommentsListQueryVm() {
                Comments = responce.Comments,
            };
        }
    }
}
