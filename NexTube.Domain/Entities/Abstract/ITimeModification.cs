namespace NexTube.Domain.Entities.Abstract {
    public interface ITimeModification
    {
        DateTime DateCreated { get; set; }
        DateTime? DateModified { get; set; }
    }
}
