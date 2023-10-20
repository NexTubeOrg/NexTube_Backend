using MediatR;
using NexTube.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Photos.Queries.GetPhoto
{
    public class GetPhotoQueryHandler : IRequestHandler<GetPhotoQuery, GetPhotoQueryVm>
    {
        private readonly IPhotoService _photoService;

        public GetPhotoQueryHandler(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task<GetPhotoQueryVm> Handle(GetPhotoQuery request, CancellationToken cancellationToken)
        {
            var result = await _photoService.GetPhoto(request.PhotoId);

            var getPhotoQueryVm = new GetPhotoQueryVm()
            {
                PhotoStream = result.Stream,
                ContentType = result.ContentType,
            };

            return getPhotoQueryVm;
        }
    }
}
