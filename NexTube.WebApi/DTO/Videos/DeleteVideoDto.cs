using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Videos.Commands.DeleteVideo;

namespace NexTube.WebApi.DTO.Videos
{
    public class DeleteVideoDto : IMapWith<DeleteVideoCommand>
    {
        public int VideoId { get; set; } = 0;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteVideoDto, DeleteVideoCommand>()
                .ForMember(command => command.VideoId, opt => opt.MapFrom(dto => dto.VideoId));
        }
    }
}
