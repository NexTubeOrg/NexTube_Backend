using MediatR;
using NexTube.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Photos.Commands.UploadPhoto
{
    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, string>
    {
        private readonly IPhotoService _photoService;

        public UploadPhotoCommandHandler(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task<string> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var result = await _photoService.UploadPhoto(request.Source);
            return result.PhotoId;
        }
    }
}
