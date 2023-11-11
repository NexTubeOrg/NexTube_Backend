using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity
{
    public class GetVideoEntityQueryHandler : IRequestHandler<GetVideoEntityQuery, GetVideoEntityQueryVm>
    {
        private readonly IVideoService _videoService;

        public GetVideoEntityQueryHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<GetVideoEntityQueryVm> Handle(GetVideoEntityQuery request, CancellationToken cancellationToken)
        {
            var result = await _videoService.GetVideoEntity(request.VideoEntityId);

            var GetVideoEntityQueryVm = new GetVideoEntityQueryVm()
            {
                VideoEntity = result.VideoEntity
            };

            return GetVideoEntityQueryVm;
        }
    }
}
