using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetAllVideoEntities
{
    public class GetAllVideoEntitiesQueryHandler : IRequestHandler<GetAllVideoEntitiesQuery, GetAllVideoEntitiesQueryResult>
    {
        private readonly IVideoService _videoService;

        public GetAllVideoEntitiesQueryHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<GetAllVideoEntitiesQueryResult> Handle(GetAllVideoEntitiesQuery request, CancellationToken cancellationToken)
        {
            var result = await _videoService.GetAllVideoEntities();

            var GetAllVideoEntitiesQueryResult = new GetAllVideoEntitiesQueryResult()
            {
                VideoEntities = result.VideoEntities,
            };

            return GetAllVideoEntitiesQueryResult;
        }
    }
}
