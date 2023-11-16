using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Files.Photos.Queries.GetPhotoUrl
{
    public class GetPhotoUrlQueryHandler : IRequestHandler<GetPhotoUrlQuery, GetPhotoUrlQueryResult>
    {
        private readonly IPhotoService _photoService;

        public GetPhotoUrlQueryHandler(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task<GetPhotoUrlQueryResult> Handle(GetPhotoUrlQuery request, CancellationToken cancellationToken)
        {
            string url = string.Empty;

            if(request.Size is null)
                url = (await _photoService.GetPhotoUrl(request.PhotoId)).Url;
            else
                url = (await _photoService.GetPhotoUrl(request.PhotoId, request.Size.Value)).Url;

            var GetPhotoUrlQueryResult = new GetPhotoUrlQueryResult()
            {
                PhotoUrl = url,
            };

            return GetPhotoUrlQueryResult;
        }
    }
}
