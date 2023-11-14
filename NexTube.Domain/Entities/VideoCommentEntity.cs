using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Domain.Entities {
    public class VideoCommentEntity {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;

        public VideoEntity VideoEntity { get; set; } = null!;
        public ApplicationUser Owner { get; set; } = null!;
    }
}
