using System.ComponentModel.DataAnnotations.Schema;

namespace NexTube.Domain.Entities.Abstract {
    public abstract class OwnedEntity : AuditableEntity {
        public ApplicationUser? Creator { get; set; }

        public int? CreatorId { get; set; }
    }
}
