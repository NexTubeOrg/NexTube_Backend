using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Videos.Commands.DeleteVideoById;

namespace NexTube.WebApi.DTO.Files.Video
{
    public class DeleteVideoById : IMapWith<DeleteVideoByIdCommand>
    {
        public int VideoId { get; set; } = 0;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteVideoById, DeleteVideoByIdCommand>()
                .ForMember(command => command.VideoId, opt => opt.MapFrom(dto => dto.VideoId));
        }
    }
}
