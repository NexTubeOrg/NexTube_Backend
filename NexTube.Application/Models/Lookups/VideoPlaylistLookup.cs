using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Domain.Entities;

namespace NexTube.Application.Models.Lookups {
    public class VideoPlaylistLookup : IMapWith<VideoPlaylistEntity> {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? TotalCountVideos { get; set; }
        public string? Preview { get; set; }
        public IList<VideoLookup>? Videos { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<VideoPlaylistEntity, VideoPlaylistLookup>()
                .ForMember(l => l.Title, opt => opt.MapFrom(e => e.Title))
                .ForMember(l => l.Preview, opt => opt.MapFrom(e => e.PreviewImage.ToString()));
        }
    }
}
