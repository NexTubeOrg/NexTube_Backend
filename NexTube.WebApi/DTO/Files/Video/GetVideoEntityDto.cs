using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Videos.Queries.GetVideoEntity;

namespace NexTube.WebApi.DTO.Files.Video
{
    public class GetVideoEntityDto : IMapWith<GetVideoEntityQuery>
    {
        public int VideoEntityId { get; set; } = 0;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetVideoEntityDto, GetVideoEntityQuery>()
                .ForMember(query => query.VideoEntityId, opt => opt.MapFrom(dto => dto.VideoEntityId));
        }
    }
}
