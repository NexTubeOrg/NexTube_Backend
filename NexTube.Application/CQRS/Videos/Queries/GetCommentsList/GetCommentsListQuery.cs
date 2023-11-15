using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Videos.Queries.GetCommentsList {
    public class GetCommentsListQuery : IRequest<GetCommentsListQueryVm> {
        public int? VideoId { get; set; } = null!;
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
