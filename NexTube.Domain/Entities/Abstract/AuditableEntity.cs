using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Domain.Entities.Abstract {
    public abstract class AuditableEntity : BaseEntity, ITimeModification {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
