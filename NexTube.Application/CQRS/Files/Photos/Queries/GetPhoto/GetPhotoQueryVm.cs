using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Photos.Queries.GetPhoto
{
    public class GetPhotoQueryVm
    {
        public Stream PhotoStream { get; set; } = null!;
        public string ContentType { get; set; } = null!;
    }
}
