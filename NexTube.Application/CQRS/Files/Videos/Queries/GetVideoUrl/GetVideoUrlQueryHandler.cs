using MediatR;
using NexTube.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoUrl
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
            var result = await _videoService.GetUrlVideo(request.VideoId);

            var getVideoUrlResult = new GetVideoUrlQueryResult()
            {
                VideoUrl = result.VideoUrl,
            };

            return getVideoUrlResult;
        }
    }
}
