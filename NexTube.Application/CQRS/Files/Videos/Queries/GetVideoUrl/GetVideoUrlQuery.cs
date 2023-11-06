using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoUrl
{
    public class GetVideoUrlQuery : IRequest<GetVideoUrlQueryVm>
    {
        public string VideoId { get; set; } = string.Empty;
    }
}
