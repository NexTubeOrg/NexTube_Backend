using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Videos.GetVideoFileUrl
{
    public class GetVideoUrlQueryHandler : IRequestHandler<GetVideoUrlQuery, GetVideoUrlQueryResult>
    {
        private readonly IVideoService _videoService;

        public GetVideoUrlQueryHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<GetVideoUrlQueryResult> Handle(GetVideoUrlQuery request, CancellationToken cancellationToken)
        {
            var result = await _videoService.GetUrlVideo(request.VideoFileId);

            var getVideoUrlResult = new GetVideoUrlQueryResult()
            {
                VideoUrl = result.VideoUrl,
            };

            return getVideoUrlResult;
        }
    }
}
