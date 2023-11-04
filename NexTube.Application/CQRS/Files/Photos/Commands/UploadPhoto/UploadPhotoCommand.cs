using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Photos.Commands.UploadPhoto
{
    public class UploadPhotoCommand : IRequest<string>
    {
        public Stream Source { get; set; } = null!;
    }
}
