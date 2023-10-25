using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Files.Photos.Commands.UploadPhoto;
using NexTube.Application.CQRS.Files.Videos.Commands.UploadVideo;
using NexTube.WebApi.DTO.Files.Photo;

namespace NexTube.WebApi.DTO.Files.Video
{
    public class UploadVideoDto : IMapWith<UploadVideoCommand>
    {
        public IFormFile Video { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UploadVideoDto, UploadVideoCommand>()
                .ForMember(command => command.Source, opt => opt.MapFrom(dto => dto.Video.OpenReadStream()));
        }
    }
}
