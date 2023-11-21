using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Videos.GetVideoFileUrl;

namespace NexTube.WebApi.DTO.Files.Video
{
    public class GetVideoFileUrlDto : IMapWith<GetVideoUrlQuery>
    {
        public string VideoFileId { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetVideoFileUrlDto, GetVideoUrlQuery>()
                .ForMember(query => query.VideoFileId, opt => opt.MapFrom(dto => dto.VideoFileId));
        }
    }
}
