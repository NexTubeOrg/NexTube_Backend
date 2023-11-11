namespace NexTube.Domain.Entities.Abstract {
    public abstract class OwnedEntity : AuditableEntity { 
        public ApplicationUser? Creator { get; set; }
    }
}
