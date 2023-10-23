using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Photos.Queries.GetPhotoUrl
{
    public class GetPhotoUrlQueryHandler : IRequestHandler<GetPhotoUrlQuery, GetPhotoUrlQueryVm>
    {
        private readonly IPhotoService _photoService;

        public GetPhotoUrlQueryHandler(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task<GetPhotoUrlQueryVm> Handle(GetPhotoUrlQuery request, CancellationToken cancellationToken)
        {
            var result = await _photoService.GetPhotoUrl(request.PhotoId);

            var GetPhotoUrlQueryVm = new GetPhotoUrlQueryVm()
            {
                PhotoUrl = result.Url,
            };

            return GetPhotoUrlQueryVm;
        }
    }
}
