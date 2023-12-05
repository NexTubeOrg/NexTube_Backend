using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Videos.GetVideoFileUrl;

namespace NexTube.WebApi.DTO.Videos
{
    public class GetVideoUrlDto : IMapWith<GetVideoUrlQuery>
    {
        public string VideoFileId { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetVideoUrlDto, GetVideoUrlQuery>()
                .ForMember(query => query.VideoFileId, opt => opt.MapFrom(dto => dto.VideoFileId));
        }
    }
}
