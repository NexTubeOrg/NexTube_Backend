using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Photos.Commands.UploadSquarePhoto {
    public class UploadSquarePhotoCommand : IRequest<string>
    {
        public Stream Source { get; set; } = null!;
    }
}
