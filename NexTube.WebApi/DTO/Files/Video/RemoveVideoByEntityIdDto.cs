using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Videos.Commands.RemoveVideoByEntityId;
using NexTube.Application.CQRS.Files.Videos.Queries.GetVideoUrl;

namespace NexTube.WebApi.DTO.Files.Video
{
    public class RemoveVideoByEntityIdDto : IMapWith<RemoveVideoByEntityIdCommand>
    {
        public int VideoEntityId { get; set; } = 0;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RemoveVideoByEntityIdDto, RemoveVideoByEntityIdCommand>()
                .ForMember(command => command.VideoEntityId, opt => opt.MapFrom(dto => dto.VideoEntityId));
        }
    }
}
