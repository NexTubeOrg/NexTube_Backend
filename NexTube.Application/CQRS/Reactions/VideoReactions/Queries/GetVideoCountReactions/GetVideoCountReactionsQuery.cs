using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Reactions.VideoReactions.Queries.GetVideoCountReactions {
    public class GetVideoCountReactionsQuery : IRequest<VideoReactionsCount> {
        public int VideoId { get; set; }
    }
}
