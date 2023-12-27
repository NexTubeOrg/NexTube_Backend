using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Playlists.VideoPlaylists.Queries.GetVideoPlaylistsUserStatus;

namespace NexTube.WebApi.DTO.Playlists {
    public class GetVideoPlaylistsUserStatusDto : IMapWith<GetVideoPlaylistsUserStatusQuery> {
        public int VideoId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<GetVideoPlaylistsUserStatusDto, GetVideoPlaylistsUserStatusQuery>()
                .ForMember(q => q.VideoId, opt => opt.MapFrom(dto => dto.VideoId))
                .ForMember(q => q.Page, opt => opt.MapFrom(dto => dto.Page))
                .ForMember(q => q.PageSize, opt => opt.MapFrom(dto => dto.PageSize));
        }
    }
}
