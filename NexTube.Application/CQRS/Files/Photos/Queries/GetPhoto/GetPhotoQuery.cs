using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Photos.Queries.GetPhoto
{
    public class GetPhotoQuery : IRequest<GetPhotoQueryVm>
    {
        public string PhotoId { get; set; } = null!;
    }
}
