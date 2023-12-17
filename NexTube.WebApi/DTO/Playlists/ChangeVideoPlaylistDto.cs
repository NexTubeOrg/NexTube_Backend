using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.ToggleVideoPlaylist;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Commands.CreatePlaylist;

namespace NexTube.WebApi.DTO.Playlists {
    public class ChangeVideoPlaylistDto : IMapWith<ToggleVideoPlaylistCommand> {
        public int VideoId { get; set; }
        public int PlaylistId { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<ChangeVideoPlaylistDto, ToggleVideoPlaylistCommand>()
                .ForMember(q => q.VideoId, opt => opt.MapFrom(dto => dto.VideoId))
                .ForMember(q => q.PlaylistId, opt => opt.MapFrom(dto => dto.PlaylistId));
        }
    }
}
