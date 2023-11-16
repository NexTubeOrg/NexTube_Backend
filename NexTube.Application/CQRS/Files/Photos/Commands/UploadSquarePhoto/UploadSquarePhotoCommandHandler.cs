using MediatR;
using NexTube.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Photos.Commands.UploadSquarePhoto {
    public class UploadSquarePhotoCommandHandler : IRequestHandler<UploadSquarePhotoCommand, string>
    {
        private readonly IPhotoService _photoService;

        public UploadSquarePhotoCommandHandler(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task<string> Handle(UploadSquarePhotoCommand request, CancellationToken cancellationToken)
        {
            var result = await _photoService.UploadPhoto(request.Source);
            return result.PhotoId;
        }
    }
}
