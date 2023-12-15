using AutoMapper;
using NexTube.Application.Common.Mappings;
using NexTube.Application.CQRS.Videos.Commands.UploadVideo;

namespace NexTube.WebApi.DTO.Videos
{
    public class UploadVideoDto : IMapWith<UploadVideoCommand>
    {
        public IFormFile Video { get; set; } = null!;
        public IFormFile PreviewPhoto { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? AccessModificator { get; set; } = null;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UploadVideoDto, UploadVideoCommand>()
                .ForMember(command => command.Source, opt => opt.MapFrom(dto => dto.Video.OpenReadStream()))
                .ForMember(command => command.PreviewPhotoSource, opt => opt.MapFrom(dto => dto.PreviewPhoto.OpenReadStream()))
                .ForMember(command => command.Name, opt => opt.MapFrom(dto => dto.Name))
                .ForMember(command => command.Description, opt => opt.MapFrom(dto => dto.Description))
                .ForMember(command => command.AccessModificator, opt => opt.MapFrom(dto => dto.AccessModificator));
        }
    }
}
