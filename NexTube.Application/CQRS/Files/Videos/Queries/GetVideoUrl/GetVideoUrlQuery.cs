﻿using MediatR;
using NexTube.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetVideoUrl
{
    public class GetVideoUrlQuery : IRequest<GetVideoUrlQueryResult>
    {
        public string VideoId { get; set; } = string.Empty;
    }
}
