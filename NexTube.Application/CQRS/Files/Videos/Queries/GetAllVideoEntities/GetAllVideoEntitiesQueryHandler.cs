using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetAllVideoEntities
{
    public class GetAllVideoEntitiesQueryHandler : IRequestHandler<GetAllVideoEntitiesQuery, GetAllVideoEntitiesQueryVm>
    {
        private readonly IVideoService _videoService;

        public GetAllVideoEntitiesQueryHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<GetAllVideoEntitiesQueryVm> Handle(GetAllVideoEntitiesQuery request, CancellationToken cancellationToken)
        {
            var result = await _videoService.GetAllVideoEntities();


            var GetAllVideoEntitiesQueryVm = new GetAllVideoEntitiesQueryVm()
            {
                VideoEntities = result.VideoEntities,
            };

            return GetAllVideoEntitiesQueryVm;
        }
    }
}
