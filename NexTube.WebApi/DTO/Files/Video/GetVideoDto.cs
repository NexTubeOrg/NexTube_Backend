using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Videos.Queries.GetVideoById;

namespace NexTube.WebApi.DTO.Files.Video
{
    public class GetVideoDto : IMapWith<GetVideoByIdQuery>
    {
        public int VideoId { get; set; } = 0;
        public int? UserId { get; set; } = null;


        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetVideoDto, GetVideoByIdQuery>()
                .ForMember(query => query.VideoId, opt => opt.MapFrom(dto => dto.VideoId))
                .ForMember(query => query.UserId, opt => opt.MapFrom(dto => dto.UserId));
        }
    }
}
