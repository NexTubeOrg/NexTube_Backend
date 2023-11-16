using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity
{
    public class GetVideoEntityQueryHandler : IRequestHandler<GetVideoEntityQuery, GetVideoEntityQueryResult>
    {
        private readonly IVideoService _videoService;

        public GetVideoEntityQueryHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<GetVideoEntityQueryResult> Handle(GetVideoEntityQuery request, CancellationToken cancellationToken)
        {
            var result = await _videoService.GetVideoEntity(request.VideoEntityId);

            var GetVideoEntityQueryResult = new GetVideoEntityQueryResult()
            {
                VideoEntity = result.VideoEntity
            };

            return GetVideoEntityQueryResult;
        }
    }
}
