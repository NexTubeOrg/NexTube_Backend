using MediatR;
using NexTube.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoUrl
{
    public class GetVideoUrlQueryHandler : IRequestHandler<GetVideoUrlQuery, GetVideoUrlQueryVm>
    {
        private readonly IVideoService _videoService;

        public GetVideoUrlQueryHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<GetVideoUrlQueryVm> Handle(GetVideoUrlQuery request, CancellationToken cancellationToken)
        {
            var result = await _videoService.GetUrlVideo(request.VideoId);

            var getVideoUrlVm = new GetVideoUrlQueryVm()
            {
                VideoUrl = result.VideoUrl,
            };

            return getVideoUrlVm;
        }
    }
}
