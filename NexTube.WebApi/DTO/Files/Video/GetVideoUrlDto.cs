using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Videos.Queries.GetVideoUrl;

namespace NexTube.WebApi.DTO.Files.Video
{
    public class GetVideoUrlDto : IMapWith<GetVideoUrlQuery>
    {
        public string VideoUrl { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetVideoUrlDto, GetVideoUrlQuery>()
                .ForMember(query => query.VideoId, opt => opt.MapFrom(dto => dto.VideoUrl));
        }
    }
}
