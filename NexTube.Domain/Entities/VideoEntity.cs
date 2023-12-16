using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities {
    public class VideoEntity : OwnedEntity {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid? VideoFileId { get; set; } = null!;
        public Guid? PreviewPhotoFileId { get; set; } = null!;
        public int Views { get; set; } = 0;

        public VideoAccessModificatorEntity? AccessModificator { get; set; } = null!;

        /// <summary>
        ///  May be null, so video could be not in a playlist
        /// </summary>
        public VideoPlaylistEntity? Playlist { get; set; } = null;
        public int? PlaylistId { get; set; }
    }
}
