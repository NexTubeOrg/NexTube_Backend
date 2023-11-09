using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Videos.Queries.GetCommentsList {
    public class GetCommentsListQueryVm {
        public IList<CommentLookup> Comments { get; set; } = new List<CommentLookup>();
    }
}
