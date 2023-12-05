using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Videos.Commands.UpdateVideo;

namespace NexTube.WebApi.DTO.Videos
{
    public class UpdateVideoDto : IMapWith<UpdateVideoCommand>
    {
        public int VideoId { get; set; } = 0;
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string? AccessModificator { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateVideoDto, UpdateVideoCommand>()
                .ForMember(command => command.VideoId, opt => opt.MapFrom(dto => dto.VideoId))
                .ForMember(command => command.AccessModificator, opt => opt.MapFrom(dto => dto.AccessModificator))
                .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name))
                .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description));
        }
    }
}
